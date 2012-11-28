#import <UIKit/UIKit.h>

#include "RegisterClasses.h"
#include "RegisterMonoModules.h"

// Hack to work around iOS SDK 4.3 linker problem
// we need at least one __TEXT, __const section entry in main application .o files
// to get this section emitted at right time and so avoid LC_ENCRYPTION_INFO size miscalculation
static const int constsection = 0;
void UnityParseCommandLine(int argc, char *argv[]);

int main(int argc, char *argv[])
{
	UnityParseCommandLine(argc, argv);

	RegisterMonoModules();
	NSLog(@"-> registered mono modules %p\n", &constsection);
	
	NSAutoreleasePool*		pool = [NSAutoreleasePool new];
	
	UIApplicationMain(argc, argv, nil, @"AppController");
	
	[pool release];
	
	return 0;
}
