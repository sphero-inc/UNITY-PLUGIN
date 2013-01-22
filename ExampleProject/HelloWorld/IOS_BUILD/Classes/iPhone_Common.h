
#ifndef _TRAMPOLINE_IPHONE_COMMON_H_
#define _TRAMPOLINE_IPHONE_COMMON_H_

#define ENABLE_UNITY_DEBUG_LOG 0


extern	bool	_ios30orNewer;
extern	bool	_ios31orNewer;
extern	bool	_ios43orNewer;
extern	bool	_ios50orNewer;
extern	bool	_ios60orNewer;

struct UnityFrameStats;


//------------------------------------------------------------------------------

#if ENABLE_UNITY_DEBUG_LOG
	#define UNITY_DBG_LOG(...)				\
		do 									\
		{									\
			printf_console(__VA_ARGS__);	\
		}									\
		while(0)
#else
	#define UNITY_DBG_LOG(...)				\
		do 									\
		{									\
		}									\
		while(0)
#endif // ENABLE_UNITY_DEBUG_LOG


#endif // _TRAMPOLINE_IPHONE_COMMON_H_
