
#include "iPhone_Profiler.h"

#include <mach/mach_time.h>
#include <stdio.h>

void*	UnityCreateProfilerCounter( const char* name );
void	UnityDestroyProfilerCounter( void* counter );
void	UnityStartProfilerCounter( void* counter );
void	UnityEndProfilerCounter( void* counter );
void 	UnityFinishRendering();


#if ENABLE_INTERNAL_PROFILER


struct UnityFrameStats
{
	typedef signed long long Timestamp;

	Timestamp fixedBehaviourManagerDt;
	Timestamp fixedPhysicsManagerDt;
	Timestamp dynamicBehaviourManagerDt;
	Timestamp coroutineDt;
	Timestamp skinMeshUpdateDt;
	Timestamp animationUpdateDt;
	Timestamp renderDt;
	Timestamp cullingDt;
	Timestamp clearDt;
	int fixedUpdateCount;

	// TODO: add msaa resolve in here

	Timestamp drawCallTime;
	int drawCallCount;
	int triCount;
	int vertCount;

	Timestamp batchDt;
	int batchedDrawCallCount;
	int batchedTris;
	int batchedVerts;
};

extern "C"
{
	long mono_gc_get_used_size();
	long mono_gc_get_heap_size();

	extern void* GC_notify_event;
	extern void* GC_on_heap_resize;

	typedef enum
	{
		MONO_GC_EVENT_START,
		MONO_GC_EVENT_MARK_START,
		MONO_GC_EVENT_MARK_END,
		MONO_GC_EVENT_RECLAIM_START,
		MONO_GC_EVENT_RECLAIM_END,
		MONO_GC_EVENT_END,
		MONO_GC_EVENT_PRE_STOP_WORLD,
		MONO_GC_EVENT_POST_STOP_WORLD,
		MONO_GC_EVENT_PRE_START_WORLD,
		MONO_GC_EVENT_POST_START_WORLD
	} MonoGCEvent;
}

extern bool	_ios43orNewer;

namespace
{
	typedef signed long long	Prof_Int64;

	mach_timebase_info_data_t info;
	void ProfilerInit()
	{
		mach_timebase_info(&info);
	}

	static float MachToMillisecondsDelta (Prof_Int64 delta)
	{
		// Convert to nanoseconds
		delta *= info.numer;
		delta /= info.denom;
		float result = (float)delta / 1000000.0F;
		return result;
	}

	struct ProfilerBlock
	{
		Prof_Int64 maxV, minV, avgV;
	};

	void ProfilerBlock_Update(struct ProfilerBlock* b, Prof_Int64 d, bool reset, bool avoidZero = false)
	{
		if (reset)
		{
			b->maxV = b->minV = b->avgV = d;
		}
		else
		{
			b->maxV = (d > b->maxV)? d : b->maxV;
			if (avoidZero && (b->minV == 0 || d == 0))
				b->minV = (d > b->minV)? d : b->minV;
			else
				b->minV = (d < b->minV)? d : b->minV;
			b->avgV += d;
		}
	}


	int _frameId = 0;

	struct ProfilerBlock _framePB;
	struct ProfilerBlock _gpuPB;
	struct ProfilerBlock _swapPB;
	struct ProfilerBlock _playerPB;
	struct ProfilerBlock _oglesPB;

	struct ProfilerBlock _drawCallCountPB;
	struct ProfilerBlock _triCountPB;
	struct ProfilerBlock _vertCountPB;

	struct ProfilerBlock _batchPB;
	struct ProfilerBlock _batchedDrawCallCountPB;
	struct ProfilerBlock _batchedTriCountPB;
	struct ProfilerBlock _batchedVertCountPB;

	struct ProfilerBlock _fixedBehaviourManagerPB;
	struct ProfilerBlock _fixedPhysicsManagerPB;
	struct ProfilerBlock _dynamicBehaviourManagerPB;
	struct ProfilerBlock _coroutinePB;
	struct ProfilerBlock _skinMeshUpdatePB;
	struct ProfilerBlock _animationUpdatePB;
	struct ProfilerBlock _unityRenderLoopPB;
	struct ProfilerBlock _unityCullingPB;
	struct ProfilerBlock _unityWaitsForGpuPB;
	struct ProfilerBlock _unityMSAAResolvePB;
	struct ProfilerBlock _fixedUpdateCountPB;
	struct ProfilerBlock _GCCountPB;
	struct ProfilerBlock _GCDurationPB;


	Prof_Int64 _gpuDelta			= 0;
	Prof_Int64 _swapStart			= 0;
	Prof_Int64 _lastVBlankTime 		= -1;

	Prof_Int64 _frameStart			= 0;
	Prof_Int64 _loopStart			= 0;
	Prof_Int64 _frameStartToLoopEnd	= 0;
	Prof_Int64 _loopStartToFrameEnd = 0;

	Prof_Int64 _msaaResolveStart	= 0;
	Prof_Int64 _msaaResolve			= 0;
	void*	   _msaaResolveCounter	= 0;


	struct UnityFrameStats _unityFrameStats;


	Prof_Int64 gcstarted = 0;
	void gccallback(int event)
	{
		if (event == MONO_GC_EVENT_START)
			gcstarted = mach_absolute_time();

		if (event == MONO_GC_EVENT_END)
		{
			float delta = mach_absolute_time() - gcstarted;
			ProfilerBlock_Update(&_GCDurationPB, delta, false);
			ProfilerBlock_Update(&_GCCountPB, 1, false);
		}
	}
}

void Profiler_InitProfiler()
{
	GC_notify_event = (void*)gccallback;
	ProfilerInit();

	if( _msaaResolveCounter == 0 )
		_msaaResolveCounter = UnityCreateProfilerCounter("iOS.MSAAResolve");
}

void Profiler_UninitProfiler()
{
	UnityDestroyProfilerCounter(_msaaResolveCounter);
}

void
Profiler_UnityLoopStart()
{
	_loopStart = mach_absolute_time();
}

void
Profiler_UnityLoopEnd()
{
	_frameStartToLoopEnd = mach_absolute_time() - _frameStart;
}

void
Profiler_FrameStart()
{
	_frameStart = mach_absolute_time();
}

void
Profiler_FrameEnd()
{
	if (_frameId % BLOCK_ON_GPU_EACH_NTH_FRAME == (BLOCK_ON_GPU_EACH_NTH_FRAME-1))
	{
		Prof_Int64 gpuTime0 = mach_absolute_time();

#if ENABLE_BLOCK_ON_GPU_PROFILER
		UnityFinishRendering();
#endif

		Prof_Int64 gpuTime1 = mach_absolute_time();
		_gpuDelta = gpuTime1 - gpuTime0;
	}
	else
	{
		_gpuDelta = 0;
	}

	_swapStart 			 = mach_absolute_time();
	_loopStartToFrameEnd = _swapStart - _loopStart;
}

void
Profiler_FrameUpdate(const struct UnityFrameStats* unityFrameStats)
{
	_unityFrameStats = *unityFrameStats;

	Prof_Int64 vblankTime = mach_absolute_time();

	static bool firstFrame = true;
	if( firstFrame )
	{
		_lastVBlankTime = vblankTime;

		firstFrame = false;
		return; // skip first frame
	}

	Prof_Int64 frameDelta  = vblankTime - _lastVBlankTime;
	Prof_Int64 swapDelta   = vblankTime - _swapStart;
	Prof_Int64 playerDelta = _loopStartToFrameEnd + _frameStartToLoopEnd - _gpuDelta - _unityFrameStats.drawCallTime;

	_lastVBlankTime = vblankTime;

	const int EachNthFrame = 30;
	if (_frameId == EachNthFrame)
	{
		_frameId = 0;

		printf_console("iPhone Unity internal profiler stats:\n");
		printf_console("cpu-player>    min: %4.1f   max: %4.1f   avg: %4.1f\n", MachToMillisecondsDelta(_playerPB.minV), MachToMillisecondsDelta(_playerPB.maxV), MachToMillisecondsDelta(_playerPB.avgV / EachNthFrame));
		printf_console("cpu-ogles-drv> min: %4.1f   max: %4.1f   avg: %4.1f\n", MachToMillisecondsDelta(_oglesPB.minV), MachToMillisecondsDelta(_oglesPB.maxV), MachToMillisecondsDelta(_oglesPB.avgV / EachNthFrame));
#if ENABLE_BLOCK_ON_GPU_PROFILER
		printf_console("gpu>           min: %4.1f   max: %4.1f   avg: %4.1f\n", MachToMillisecondsDelta(_gpuPB.minV), MachToMillisecondsDelta(_gpuPB.maxV), MachToMillisecondsDelta((BLOCK_ON_GPU_EACH_NTH_FRAME*(int)_gpuPB.avgV) / EachNthFrame));
#endif
		// only pay attention if wait-for-gpu is significant (2 milliseconds)
		const float waitForGpuThreshold = 2.0f * EachNthFrame;
		if (MachToMillisecondsDelta(_unityWaitsForGpuPB.avgV) >= waitForGpuThreshold)
		{
			printf_console("cpu-waits-gpu> min: %4.1f   max: %4.1f   avg: %4.1f\n", MachToMillisecondsDelta(_unityWaitsForGpuPB.minV), MachToMillisecondsDelta(_unityWaitsForGpuPB.maxV), MachToMillisecondsDelta(_unityWaitsForGpuPB.avgV / EachNthFrame));
			printf_console(" msaa-resolve> min: %4.1f   max: %4.1f   avg: %4.1f\n", MachToMillisecondsDelta(_unityMSAAResolvePB.minV), MachToMillisecondsDelta(_unityMSAAResolvePB.maxV), MachToMillisecondsDelta(_unityMSAAResolvePB.avgV / EachNthFrame));
		}
		printf_console("frametime>     min: %4.1f   max: %4.1f   avg: %4.1f\n", MachToMillisecondsDelta(_framePB.minV), MachToMillisecondsDelta(_framePB.maxV), MachToMillisecondsDelta(_framePB.avgV / EachNthFrame));

		printf_console("draw-call #>   min: %3d    max: %3d    avg: %3d     | batched: %5d\n", (int)_drawCallCountPB.minV, (int)_drawCallCountPB.maxV, (int)(_drawCallCountPB.avgV / EachNthFrame), (int)(_batchedDrawCallCountPB.avgV / EachNthFrame));
		printf_console("tris #>        min: %5d  max: %5d  avg: %5d   | batched: %5d\n", (int)_triCountPB.minV, (int)_triCountPB.maxV, (int)(_triCountPB.avgV / EachNthFrame), (int)(_batchedTriCountPB.avgV / EachNthFrame));
		printf_console("verts #>       min: %5d  max: %5d  avg: %5d   | batched: %5d\n", (int)_vertCountPB.minV, (int)_vertCountPB.maxV, (int)(_vertCountPB.avgV / EachNthFrame), (int)(_batchedVertCountPB.avgV / EachNthFrame));

		printf_console("player-detail> physx: %4.1f animation: %4.1f culling %4.1f skinning: %4.1f batching: %4.1f render: %4.1f fixed-update-count: %d .. %d\n",
					   MachToMillisecondsDelta((int)_fixedPhysicsManagerPB.avgV / EachNthFrame),
					   MachToMillisecondsDelta((int)_animationUpdatePB.avgV / EachNthFrame),
					   MachToMillisecondsDelta((int)_unityCullingPB.avgV / EachNthFrame),
					   MachToMillisecondsDelta((int)_skinMeshUpdatePB.avgV / EachNthFrame),
					   MachToMillisecondsDelta((int)_batchPB.avgV / EachNthFrame),
#if INCLUDE_OPENGLES_IN_RENDER_TIME
					   MachToMillisecondsDelta((int)(_unityRenderLoopPB.avgV - _batchPB.avgV - _unityCullingPB.avgV - _unityWaitsForGpuPB.avgV) / EachNthFrame),
#else
					   MachToMillisecondsDelta((int)(_unityRenderLoopPB.avgV - _oglesPB.avgV - _batchPB.avgV - _unityCullingPB.avgV - _unityWaitsForGpuPB.avgV) / EachNthFrame),
#endif
					   (int)_fixedUpdateCountPB.minV, (int)_fixedUpdateCountPB.maxV);
		printf_console("mono-scripts>  update: %4.1f   fixedUpdate: %4.1f coroutines: %4.1f \n", MachToMillisecondsDelta(_dynamicBehaviourManagerPB.avgV / EachNthFrame), MachToMillisecondsDelta(_fixedBehaviourManagerPB.avgV / EachNthFrame), MachToMillisecondsDelta(_coroutinePB.avgV / EachNthFrame));
		printf_console("mono-memory>   used heap: %d allocated heap: %d  max number of collections: %d collection total duration: %4.1f\n", mono_gc_get_used_size(), mono_gc_get_heap_size(), (int)_GCCountPB.avgV, MachToMillisecondsDelta(_GCDurationPB.avgV));
		printf_console("----------------------------------------\n");
	}
	ProfilerBlock_Update(&_framePB, frameDelta, (_frameId == 0));
	ProfilerBlock_Update(&_swapPB, swapDelta, (_frameId == 0));

	ProfilerBlock_Update(&_gpuPB, _gpuDelta, (_frameId == 0), true);
	ProfilerBlock_Update(&_playerPB, playerDelta, (_frameId == 0));
	ProfilerBlock_Update(&_oglesPB, _unityFrameStats.drawCallTime, (_frameId == 0));

	ProfilerBlock_Update(&_drawCallCountPB, _unityFrameStats.drawCallCount, (_frameId == 0));
	ProfilerBlock_Update(&_triCountPB, _unityFrameStats.triCount, (_frameId == 0));
	ProfilerBlock_Update(&_vertCountPB, _unityFrameStats.vertCount, (_frameId == 0));

	ProfilerBlock_Update(&_batchPB, _unityFrameStats.batchDt, (_frameId == 0));
	ProfilerBlock_Update(&_batchedDrawCallCountPB, _unityFrameStats.batchedDrawCallCount, (_frameId == 0));
	ProfilerBlock_Update(&_batchedTriCountPB, _unityFrameStats.batchedTris, (_frameId == 0));
	ProfilerBlock_Update(&_batchedVertCountPB, _unityFrameStats.batchedVerts, (_frameId == 0));

	ProfilerBlock_Update(&_fixedBehaviourManagerPB, _unityFrameStats.fixedBehaviourManagerDt, (_frameId == 0));
	ProfilerBlock_Update(&_fixedPhysicsManagerPB, _unityFrameStats.fixedPhysicsManagerDt, (_frameId == 0));
	ProfilerBlock_Update(&_dynamicBehaviourManagerPB, _unityFrameStats.dynamicBehaviourManagerDt, (_frameId == 0));
	ProfilerBlock_Update(&_coroutinePB, _unityFrameStats.coroutineDt, (_frameId == 0));
	ProfilerBlock_Update(&_skinMeshUpdatePB, _unityFrameStats.skinMeshUpdateDt, (_frameId == 0));
	ProfilerBlock_Update(&_animationUpdatePB, _unityFrameStats.animationUpdateDt, (_frameId == 0));
	ProfilerBlock_Update(&_unityRenderLoopPB, _unityFrameStats.renderDt, (_frameId == 0));
	ProfilerBlock_Update(&_unityCullingPB, _unityFrameStats.cullingDt, (_frameId == 0));
	ProfilerBlock_Update(&_unityMSAAResolvePB, _msaaResolve, (_frameId == 0));
	ProfilerBlock_Update(&_fixedUpdateCountPB, _unityFrameStats.fixedUpdateCount, (_frameId == 0));
	ProfilerBlock_Update(&_GCCountPB, 0, (_frameId == 0));
	ProfilerBlock_Update(&_GCDurationPB, 0, (_frameId == 0));

	if( _ios43orNewer )
		ProfilerBlock_Update(&_unityWaitsForGpuPB, swapDelta, (_frameId == 0));
	else
		ProfilerBlock_Update(&_unityWaitsForGpuPB, _unityFrameStats.clearDt+_msaaResolve, (_frameId == 0));

	_msaaResolve = 0;


	++_frameId;
}

void Profiler_StartMSAAResolve()
{
	UnityStartProfilerCounter(_msaaResolveCounter);
	_msaaResolveStart = mach_absolute_time();
}

void Profiler_EndMSAAResolve()
{
	_msaaResolve += (mach_absolute_time() - _msaaResolveStart);
	UnityEndProfilerCounter(_msaaResolveCounter);
}

#endif // ENABLE_INTERNAL_PROFILER
