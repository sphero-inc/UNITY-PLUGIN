
#ifndef _TRAMPOLINE_IPHONE_GLESSUPPORT_H_
#define _TRAMPOLINE_IPHONE_GLESSUPPORT_H_

#include <OpenGLES/ES1/gl.h>
#include "iPhone_Common.h"


#define ENABLE_UNITY_GLES_DEBUG 1
#define MSAA_DEFAULT_SAMPLE_COUNT 0

struct EAGLSurfaceDesc
{
	GLuint		format;
	GLuint		depthFormat;
	GLuint		framebuffer;
	GLuint		renderbuffer;
	GLuint		depthbuffer;

	GLuint		msaaSamples;
	GLuint		msaaFramebuffer;
	GLuint		msaaRenderbuffer;
	GLuint		msaaDepthbuffer;

	float		w, h;

	void*		eaglLayer;

	bool		use32bitColor;
};
extern	EAGLSurfaceDesc	_surface;


extern 	bool			_supportsDiscard;
extern 	bool			_supportsMSAA;


void InitGLES();

void CreateSurfaceGLES(EAGLSurfaceDesc* surface);
void DestroySurfaceGLES(EAGLSurfaceDesc* surface);
void CreateSurfaceMultisampleBuffersGLES(EAGLSurfaceDesc* surface);
void DestroySurfaceMultisampleBuffersGLES(EAGLSurfaceDesc* surface);
void PreparePresentSurfaceGLES(EAGLSurfaceDesc* surface);
void AfterPresentSurfaceGLES(EAGLSurfaceDesc* surface);


void CheckGLESError(const char* file, int line);


#if ENABLE_UNITY_GLES_DEBUG
	#define GLESAssert()	do { CheckGLESError (__FILE__, __LINE__); } while(0)
	#define GLES_CHK(expr)	do { {expr;} GLESAssert(); } while(0)
#else
	#define GLESAssert()	do { } while(0)
	#define GLES_CHK(expr)	do { expr; } while(0)
#endif


#endif
