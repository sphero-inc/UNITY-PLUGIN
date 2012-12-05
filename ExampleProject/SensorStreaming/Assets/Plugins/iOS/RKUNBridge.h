//
//  RKUNBridge.h
//  Unity-iPhone
//
//  Created by Jon Carroll on 6/4/12.
//  Copyright (c) 2012 Orbotix, Inc. All rights reserved.
//

#import <Foundation/Foundation.h>

extern "C" {

    typedef void (*ReceiveDeviceMessageCallback)(const char *);
}


@interface RKUNBridge : NSObject {
    BOOL robotOnline;
    BOOL dataStreamingOn;
}

@property  ReceiveDeviceMessageCallback receiveDeviceMessageCallback;

+(RKUNBridge*)sharedBridge;

-(void)connectToRobot;
-(BOOL)isRobotOnline;

-(void)enableDataStreaming;
-(void)disableDataStreaming;

@end
