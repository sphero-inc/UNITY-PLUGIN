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
    BOOL robotInitialized;
    BOOL robotOnline;
    BOOL controllerStreamingOn;
}

@property  ReceiveDeviceMessageCallback receiveDeviceMessageCallback;

+(RKUNBridge*)sharedBridge;

-(void)connectToRobot;
-(BOOL)isRobotOnline;

- (void)setDataStreamingWithSampleRateDivisor:(uint16_t)divisor
                                 packetFrames:(uint16_t)frames
                                   sensorMask:(uint64_t)mask
                                  packetCount:(uint8_t)count;
-(void)enableControllerStreamingWithSampleRateDivisor:(uint16_t)divisor
                                         packetFrames:(uint16_t)frames
                                           sensorMask:(uint64_t)mask;
-(void)disableCotrollerStreaming;

@end
