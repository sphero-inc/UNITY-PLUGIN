//
//  RKUNBridge.m
//  Unity-iPhone
//
//  Created by Jon Carroll on 6/4/12.
//  Copyright (c) 2012 Orbotix, Inc. All rights reserved.
//

#import "RKUNBridge.h"
#import <RobotKit/RobotKit.h>

static RKUNBridge *sharedBridge = nil;
void UnityPause(bool pause);

@implementation RKUNBridge

@synthesize lastData;

-(id)init {
    self = [super init];
    
    robotOnline = NO;
    dataStreamingOn = NO;
    
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(appWillEnterBackground) name:UIApplicationWillTerminateNotification object:nil];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(appWillEnterBackground) name:UIApplicationWillResignActiveNotification object:nil];
    
    return self;
}

+(RKUNBridge*)sharedBridge {
    if(sharedBridge==nil) {
        sharedBridge = [[RKUNBridge alloc] init];
    }
    return sharedBridge;
}


-(BOOL)isRobotOnline {
    return robotOnline;
}

-(void)connectToRobot {
    /*Try to connect to the robot*/
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(handleRobotOnline) name:RKDeviceConnectionOnlineNotification object:nil];
    if ([[RKRobotProvider sharedRobotProvider] isRobotUnderControl]) {
        [[RKRobotProvider sharedRobotProvider] openRobotConnection];        
    }
}

-(void)appWillEnterBackground {
    [self disableDataStreaming];
    [RKBackLEDOutputCommand sendCommandWithBrightness:0.0];
    [[RKRobotProvider sharedRobotProvider] closeRobotConnection];
}

- (void)handleRobotOnline { 
    robotOnline = YES;
}

-(void)enableDataStreaming {
    if(dataStreamingOn && !robotOnline) return;    
    
    [[RKDeviceMessenger sharedMessenger] addDataStreamingObserver:self selector:@selector(handleDataStreaming:)];
    
    [RKStabilizationCommand sendCommandWithState:RKStabilizationStateOff];
    [RKBackLEDOutputCommand sendCommandWithBrightness:1.0];
    [RKRGBLEDOutputCommand sendCommandWithRed:1.0 green:1.0 blue:1.0];
    [RKSetDataStreamingCommand sendCommandWithSampleRateDivisor:20 packetFrames:1 sensorMask:RKDataStreamingMaskAccelerometerXFiltered | RKDataStreamingMaskAccelerometerYFiltered | RKDataStreamingMaskAccelerometerZFiltered | RKDataStreamingMaskIMUPitchAngleFiltered | RKDataStreamingMaskIMURollAngleFiltered | RKDataStreamingMaskIMUYawAngleFiltered  packetCount:0];
    
    dataStreamingOn = YES;
    
}

-(void)disableDataStreaming {
    //if(!dataStreamingOn) return;
    
    [[RKDeviceMessenger sharedMessenger] removeDataStreamingObserver:self];
    
    [RKSetDataStreamingCommand sendCommandWithSampleRateDivisor:0 packetFrames:0 sensorMask:0  packetCount:0];
    [RKStabilizationCommand sendCommandWithState:RKStabilizationStateOn];
    dataStreamingOn = NO;
    
}

- (void)handleDataStreaming:(RKDeviceAsyncData *)data
{
    if ([data isKindOfClass:[RKDeviceSensorsAsyncData class]]) {
        RKDeviceSensorsAsyncData *sensors_data = (RKDeviceSensorsAsyncData *)data;
        //NSLog(@"sensors_data.dataFrames - %@", sensors_data.dataFrames);

        RKUNData sensorData;
        
        //lastYaw = 0.0;
        for (RKDeviceSensorsData *data in sensors_data.dataFrames) {
            RKAccelerometerData *accelerometer_data = data.accelerometerData;
            sensorData.x = accelerometer_data.acceleration.x;
            sensorData.y = accelerometer_data.acceleration.y;
            sensorData.z = accelerometer_data.acceleration.z;
        }
        sensorData.yaw = [[[sensors_data.dataFrames objectAtIndex:0] attitudeData] yaw];
        sensorData.pitch = [[[sensors_data.dataFrames objectAtIndex:0] attitudeData] pitch];
        sensorData.roll = [[[sensors_data.dataFrames objectAtIndex:0] attitudeData] roll];
        
        self.lastData = sensorData;
        
    }
}

extern "C" {
    
    void _RKUNSetupRobotConnection() {
        [[RKUNBridge sharedBridge] connectToRobot];
    }
    
    bool _RKUNIsRobotConnected() {
        return [[RKUNBridge sharedBridge] isRobotOnline];
    }
    
    void _RKUNRGB(float red, float green, float blue) {
        [RKRGBLEDOutputCommand sendCommandWithRed:red green:green blue:blue];
    }
    
    void _RKUNRoll(int heading, float speed) {
        [RKRollCommand sendCommandWithHeading:heading velocity:speed];
    }
    
    
    struct RKUNData _RKUNSensorData() {
        return [RKUNBridge sharedBridge].lastData;
    }
    
    void _enableDataStreaming() {
        [[RKUNBridge sharedBridge] enableDataStreaming];
    }
    
    void _disableDataStreaming() {
        [[RKUNBridge sharedBridge] disableDataStreaming];
    }
    
    void _RKUNCalibrate(int heading) {
        [RKCalibrateCommand sendCommandWithHeading:heading];
    }
    
    void _RKUNBackLED(float intensity) {
        [RKBackLEDOutputCommand sendCommandWithBrightness:intensity];
    }
    
	
}

@end

