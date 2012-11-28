//
//  RKUNBridge.h
//  Unity-iPhone
//
//  Created by Jon Carroll on 6/4/12.
//  Copyright (c) 2012 Orbotix, Inc. All rights reserved.
//

#import <Foundation/Foundation.h>

extern "C" {
    typedef struct RKUNData {
        float pitch;
        float roll;
        float yaw;
        float x;
        float y;
        float z;
    } RKUNData;

    typedef void (*ReceiveDeviceMessageCallback)(const char *);
}


@interface RKUNBridge : NSObject {
    BOOL robotOnline;
    BOOL dataStreamingOn;
    RKUNData lastData;

}

@property (atomic) RKUNData lastData;

@property  ReceiveDeviceMessageCallback receiveDeviceMessageCallback;

+(RKUNBridge*)sharedBridge;

-(void)connectToRobot;
-(BOOL)isRobotOnline;

-(void)enableDataStreaming;
-(void)disableDataStreaming;

@end
