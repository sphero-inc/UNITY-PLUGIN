//
//  iPhone_target2AppDelegate.m
//  iPhone-target2
//
//  Created by Renaldas on 8/22/08.
//  Copyright __MyCompanyName__ 2008. All rights reserved.
//

#import "iPhone_target2AppDelegate.h"

@implementation iPhone_target2AppDelegate

@synthesize window;

- (void)applicationDidFinishLaunching:(UIApplication *)application {	
	
	// Override point for customization after app launch	
    [window makeKeyAndVisible];
}


- (void)dealloc {
	[window release];
	[super dealloc];
}


@end
