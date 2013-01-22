
#include <OpenGLES/ES1/gl.h>
#include <OpenGLES/ES1/glext.h>
#include <OpenGLES/ES2/glext.h>

#include <stdio.h>

#include "iPhone_GlesSupport.h"
#include "iPhone_Profiler.h"


void	UnityCaptureScreenshot();
bool	UnityIsCaptureScreenshotRequested();

bool 	UnityHasRenderingAPIExtension(const char* extension);
int 	UnityGetDesiredMSAASampleCount(int defaultSampleCount);

bool	UnityUse32bitDisplayBuffer();


extern 	GLint	gDefaultFBO;


extern "C" void InitEAGLLayer(void* eaglLayer, bool use32bitColor);
extern "C" bool AllocateRenderBufferStorageFromEAGLLayer(void* eaglLayer);
extern "C" void DeallocateRenderBufferStorageFromEAGLLayer();


void InitGLES()
{
#if GL_EXT_discard_framebuffer
	_supportsDiscard = UnityHasRenderingAPIExtension("GL_EXT_discard_framebuffer");
#endif

#if GL_APPLE_framebuffer_multisample
	_supportsMSAA = UnityHasRenderingAPIExtension("GL_APPLE_framebuffer_multisample");
#endif
}

void CreateSurfaceGLES(EAGLSurfaceDesc* surface)
{
	GLuint oldRenderbuffer;
	GLES_CHK( glGetIntegerv(GL_RENDERBUFFER_BINDING_OES, (GLint *) &oldRenderbuffer) );

	DestroySurfaceGLES(surface);

	InitEAGLLayer(surface->eaglLayer, surface->use32bitColor);

	GLES_CHK( glGenRenderbuffersOES(1, &surface->renderbuffer) );
	GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_OES, surface->renderbuffer) );

	if( !AllocateRenderBufferStorageFromEAGLLayer(surface->eaglLayer) )
	{
		GLES_CHK( glDeleteRenderbuffersOES(1, &surface->renderbuffer) );
		GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_BINDING_OES, oldRenderbuffer) );

		printf_console("FAILED allocating render buffer storage from gles context\n");
		return;
	}

	GLES_CHK( glGenFramebuffersOES(1, &surface->framebuffer) );

	UNITY_DBG_LOG ("glBindFramebufferOES(GL_FRAMEBUFFER_OES, %d) :: AppCtrl\n", surface->framebuffer);
	GLES_CHK( glBindFramebufferOES(GL_FRAMEBUFFER_OES, surface->framebuffer) );

	gDefaultFBO = surface->framebuffer;

	UNITY_DBG_LOG ("glFramebufferRenderbufferOES(GL_FRAMEBUFFER_OES, GL_COLOR_ATTACHMENT0_OES, GL_RENDERBUFFER_OES, %d) :: AppCtrl\n", surface->renderbuffer);
	GLES_CHK( glFramebufferRenderbufferOES(GL_FRAMEBUFFER_OES, GL_COLOR_ATTACHMENT0_OES, GL_RENDERBUFFER_OES, surface->renderbuffer) );

	CreateSurfaceMultisampleBuffersGLES(surface);
}

void DestroySurfaceGLES(EAGLSurfaceDesc* surface)
{
	if( surface->renderbuffer )
	{
		GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_OES, surface->renderbuffer) );
		DeallocateRenderBufferStorageFromEAGLLayer();

		GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_OES, 0) );
		GLES_CHK( glDeleteRenderbuffersOES(1, &surface->renderbuffer) );

		surface->renderbuffer = 0;
	}

	if( surface->framebuffer )
	{
		GLES_CHK( glDeleteFramebuffersOES(1, &surface->framebuffer) );
		surface->framebuffer = 0;
	}

	DestroySurfaceMultisampleBuffersGLES(surface);
}


void CreateSurfaceMultisampleBuffersGLES(EAGLSurfaceDesc* surface)
{
	UNITY_DBG_LOG ("CreateSurfaceMultisampleBuffers: samples=%i\n", surface->msaaSamples);
	GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_OES, surface->renderbuffer) );

	UNITY_DBG_LOG ("glBindFramebufferOES(GL_FRAMEBUFFER_OES, %d) :: AppCtrl\n", surface->framebuffer);
	GLES_CHK( glBindFramebufferOES(GL_FRAMEBUFFER_OES, surface->framebuffer) );

	gDefaultFBO = surface->framebuffer;

	DestroySurfaceMultisampleBuffersGLES(surface);

	GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_OES, surface->renderbuffer) );

	UNITY_DBG_LOG ("glBindFramebufferOES(GL_FRAMEBUFFER_OES, %d) :: AppCtrl\n", surface->framebuffer);
	GLES_CHK( glBindFramebufferOES(GL_FRAMEBUFFER_OES, surface->framebuffer) );

#if GL_APPLE_framebuffer_multisample
	if(surface->msaaSamples > 1)
	{
		GLES_CHK( glGenRenderbuffersOES(1, &surface->msaaRenderbuffer) );
		GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_OES, surface->msaaRenderbuffer) );

		GLES_CHK( glGenFramebuffersOES(1, &surface->msaaFramebuffer) );

		UNITY_DBG_LOG ("glBindFramebufferOES(GL_FRAMEBUFFER_OES, %d) :: AppCtrl\n", surface->msaaFramebuffer);
		GLES_CHK( glBindFramebufferOES(GL_FRAMEBUFFER_OES, surface->msaaFramebuffer) );

		gDefaultFBO = surface->msaaFramebuffer;

		GLES_CHK( glRenderbufferStorageMultisampleAPPLE(GL_RENDERBUFFER_OES, surface->msaaSamples, surface->format, surface->w, surface->h) );
		GLES_CHK( glFramebufferRenderbufferOES(GL_FRAMEBUFFER_OES, GL_COLOR_ATTACHMENT0_OES, GL_RENDERBUFFER_OES, surface->msaaRenderbuffer) );

		if(surface->depthFormat)
		{
			GLES_CHK( glGenRenderbuffersOES(1, &surface->msaaDepthbuffer) );
			GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_OES, surface->msaaDepthbuffer) );
			GLES_CHK( glRenderbufferStorageMultisampleAPPLE(GL_RENDERBUFFER_OES, surface->msaaSamples, GL_DEPTH_COMPONENT16_OES, surface->w, surface->h) );
			GLES_CHK( glFramebufferRenderbufferOES(GL_FRAMEBUFFER_OES, GL_DEPTH_ATTACHMENT_OES, GL_RENDERBUFFER_OES, surface->msaaDepthbuffer) );
		}
	}
	else
#endif
	{
		if(surface->depthFormat)
		{
			GLES_CHK( glGenRenderbuffersOES(1, &surface->depthbuffer) );
			GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_OES, surface->depthbuffer) );
			GLES_CHK( glRenderbufferStorageOES(GL_RENDERBUFFER_OES, surface->depthFormat, surface->w, surface->h) );

			UNITY_DBG_LOG ("glFramebufferRenderbufferOES(GL_FRAMEBUFFER_OES, GL_DEPTH_ATTACHMENT_OES, GL_RENDERBUFFER_OES, %d) :: AppCtrl\n", surface->depthbuffer);
			GLES_CHK( glFramebufferRenderbufferOES(GL_FRAMEBUFFER_OES, GL_DEPTH_ATTACHMENT_OES, GL_RENDERBUFFER_OES, surface->depthbuffer) );
		}
	}
}

void DestroySurfaceMultisampleBuffersGLES(EAGLSurfaceDesc* surface)
{
	if(surface->depthbuffer)
	{
		GLES_CHK( glDeleteRenderbuffersOES(1, &surface->depthbuffer) );
	}
	surface->depthbuffer = 0;

	if(surface->msaaDepthbuffer)
	{
		GLES_CHK( glDeleteRenderbuffersOES(1, &surface->msaaDepthbuffer) );
	}
	surface->msaaDepthbuffer = 0;

	if(surface->msaaRenderbuffer)
	{
		GLES_CHK( glDeleteRenderbuffersOES(1, &surface->msaaRenderbuffer) );
	}
	surface->msaaRenderbuffer = 0;

	if (surface->msaaFramebuffer)
	{
		GLES_CHK( glDeleteFramebuffersOES(1, &surface->msaaFramebuffer) );
	}
	surface->msaaFramebuffer = 0;
}


void PreparePresentSurfaceGLES(EAGLSurfaceDesc* surface)
{
#if GL_APPLE_framebuffer_multisample
	if( surface->msaaSamples > 0 && _supportsMSAA )
	{
		Profiler_StartMSAAResolve();

		UNITY_DBG_LOG ("  ResolveMSAA: samples=%i msaaFBO=%i destFBO=%i\n", surface->msaaSamples, surface->msaaFramebuffer, surface->framebuffer);
		GLES_CHK( glBindFramebufferOES(GL_READ_FRAMEBUFFER_APPLE, surface->msaaFramebuffer) );
		GLES_CHK( glBindFramebufferOES(GL_DRAW_FRAMEBUFFER_APPLE, surface->framebuffer) );

		GLES_CHK( glResolveMultisampleFramebufferAPPLE() );

		Profiler_EndMSAAResolve();
	}
#endif

	// update screenshot here
	if( UnityIsCaptureScreenshotRequested() )
	{
		GLint curfb = 0;
		GLES_CHK( glGetIntegerv(GL_FRAMEBUFFER_BINDING, &curfb) );
		GLES_CHK( glBindFramebufferOES(GL_FRAMEBUFFER_OES, surface->framebuffer) );
		UnityCaptureScreenshot();
		GLES_CHK( glBindFramebufferOES(GL_FRAMEBUFFER_OES, curfb) );
	}

#if GL_EXT_discard_framebuffer
	if( _supportsDiscard )
	{
		GLenum attachments[3];
		int discardCount = 0;
		if (surface->msaaSamples > 1 && _supportsMSAA)
			attachments[discardCount++] = GL_COLOR_ATTACHMENT0_OES;

		if (surface->depthFormat)
			attachments[discardCount++] = GL_DEPTH_ATTACHMENT_OES;

		attachments[discardCount++] = GL_STENCIL_ATTACHMENT_OES;

		GLenum target = (surface->msaaSamples > 1 && _supportsMSAA) ? GL_READ_FRAMEBUFFER_APPLE: GL_FRAMEBUFFER_OES;
		if (discardCount > 0)
		{
			GLES_CHK( glDiscardFramebufferEXT(target, discardCount, attachments) );
		}
	}
#endif
}

void AfterPresentSurfaceGLES(EAGLSurfaceDesc* surface)
{
	if(surface->use32bitColor != UnityUse32bitDisplayBuffer())
	{
		surface->use32bitColor = UnityUse32bitDisplayBuffer();
		CreateSurfaceGLES(surface);
		GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_OES, surface->renderbuffer) );
	}

#if GL_APPLE_framebuffer_multisample
	if (_supportsMSAA)
	{
		const int desiredMSAASamples = UnityGetDesiredMSAASampleCount(MSAA_DEFAULT_SAMPLE_COUNT);
		if (surface->msaaSamples != desiredMSAASamples)
		{
			surface->msaaSamples = desiredMSAASamples;
			CreateSurfaceMultisampleBuffersGLES(surface);
			GLES_CHK( glBindRenderbufferOES(GL_RENDERBUFFER_OES, surface->renderbuffer) );
		}

		if (surface->msaaSamples > 1)
		{
			UNITY_DBG_LOG ("  glBindFramebufferOES (GL_FRAMEBUFFER_OES, %i); // PresentSurface\n", surface->msaaFramebuffer);
			GLES_CHK( glBindFramebufferOES(GL_FRAMEBUFFER_OES, surface->msaaFramebuffer) );

			gDefaultFBO = surface->msaaFramebuffer;
		}
	}
#endif
}


extern "C" bool UnityResolveMSAA(GLuint destFBO, GLuint colorTex, GLuint colorBuf, GLuint depthTex, GLuint depthBuf)
{
#if GL_APPLE_framebuffer_multisample
	if (_surface.msaaSamples > 0 && _supportsMSAA && destFBO!=_surface.msaaFramebuffer && destFBO!=_surface.framebuffer)
	{
		Profiler_StartMSAAResolve();

		GLint oldFBO;
		GLES_CHK( glGetIntegerv (GL_FRAMEBUFFER_BINDING_OES, &oldFBO) );

		UNITY_DBG_LOG ("UnityResolveMSAA: samples=%i msaaFBO=%i destFBO=%i colorTex=%i colorRB=%i depthTex=%i depthRB=%i\n", _surface.msaaSamples, _surface.msaaFramebuffer, destFBO, colorTex, colorBuf, depthTex, depthBuf);
		UNITY_DBG_LOG ("  bind dest as DRAW FBO and textures/buffers into it\n");

		GLES_CHK( glBindFramebufferOES (GL_DRAW_FRAMEBUFFER_APPLE, destFBO) );
		if (colorTex)
			GLES_CHK( glFramebufferTexture2DOES( GL_DRAW_FRAMEBUFFER_APPLE, GL_COLOR_ATTACHMENT0, GL_TEXTURE_2D, colorTex, 0 ) );
		else if (colorBuf)
			GLES_CHK( glFramebufferRenderbufferOES (GL_DRAW_FRAMEBUFFER_APPLE, GL_COLOR_ATTACHMENT0, GL_RENDERBUFFER, colorBuf) );

		if (depthTex)
			GLES_CHK( glFramebufferTexture2DOES( GL_DRAW_FRAMEBUFFER_APPLE, GL_DEPTH_ATTACHMENT, GL_TEXTURE_2D, depthTex, 0 ) );
		else if (depthBuf)
			GLES_CHK( glFramebufferRenderbufferOES (GL_DRAW_FRAMEBUFFER_APPLE, GL_DEPTH_ATTACHMENT, GL_RENDERBUFFER, depthBuf) );

		UNITY_DBG_LOG ("  bind msaa as READ FBO\n");
		GLES_CHK( glBindFramebufferOES(GL_READ_FRAMEBUFFER_APPLE, _surface.msaaFramebuffer) );

		UNITY_DBG_LOG ("  glResolveMultisampleFramebufferAPPLE ();\n");
		GLES_CHK( glResolveMultisampleFramebufferAPPLE() );

		GLES_CHK( glBindFramebufferOES (GL_FRAMEBUFFER_OES, oldFBO) );

		Profiler_EndMSAAResolve();
		return true;
	}
	#endif
	return false;
}

extern "C" bool UnityNeedResolveMSAA(GLuint destFBO)
{
#if GL_APPLE_framebuffer_multisample
	if (_surface.msaaSamples > 0 && _supportsMSAA && destFBO!=_surface.msaaFramebuffer && destFBO!=_surface.framebuffer)
		return true;
#endif

	return false;
}


void CheckGLESError(const char* file, int line)
{
	GLenum e = glGetError();

	if( e )
	{
		printf_console ("OpenGLES error 0x%04X in %s:%i\n", e, file, line);
	}
}
