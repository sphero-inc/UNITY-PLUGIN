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
extern void UnityPause(bool pause);
extern void UnitySendMessage(const char *, const char *, const char *);

@implementation RKUNBridge


@synthesize receiveDeviceMessageCallback;

-(id)init {
    self = [super init];
    
    robotOnline = NO;
    controllerStreamingOn = NO;
    
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
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(handleDidGainControl:) name:RKRobotDidGainControlNotification object:nil];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(handleRobotOffline) name:RKDeviceConnectionOfflineNotification object:nil];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(handleRobotOffline) name:RKRobotDidLossControlNotification object:nil];
    robotInitialized = NO;

    [[RKRobotProvider sharedRobotProvider] openRobotConnection];

    robotInitialized = YES;
}

-(void)appWillEnterBackground {
    [[RKRobotProvider sharedRobotProvider] closeRobotConnection];
}

- (void)handleRobotOnline {
    RKDeviceNotification* notification = [[RKDeviceNotification alloc]initWithNotificationType:RKDeviceNotificationTypeConnected];
    // Send serialized object to Unity
    if (receiveDeviceMessageCallback != NULL) {
        RKDeviceMessageEncoder *encoder = [RKDeviceMessageEncoder encodeWithRootObject:notification];
        
        // Print a log statement to solve build bug
        if( ![encoder respondsToSelector:@selector(RKJSONRepresentation)] ) {
            NSLog(@"The application will crash, because you have not included the -all_load linker flag in the build settings of this xcodeproject");
        }
        
        receiveDeviceMessageCallback([[encoder stringRepresentation] UTF8String]);
    }
    robotOnline = YES;
}

- (void)handleRobotOffline {
    RKDeviceNotification* notification = [[RKDeviceNotification alloc]initWithNotificationType:RKDeviceNotificationTypeDisconnected];
    // Send serialized object to Unity
    if (receiveDeviceMessageCallback != NULL) {
        RKDeviceMessageEncoder *encoder = [RKDeviceMessageEncoder encodeWithRootObject:notification];
        receiveDeviceMessageCallback([[encoder stringRepresentation] UTF8String]);
    }
    robotOnline = NO;
}

-(void)handleDidGainControl:(NSNotification*)notification {
    if(!robotInitialized)return;
    [[RKRobotProvider sharedRobotProvider] openRobotConnection];
}

- (void)setDataStreamingWithSampleRateDivisor:(uint16_t)divisor
                                 packetFrames:(uint16_t)frames
                                   sensorMask:(uint64_t)mask
                                  packetCount:(uint8_t)count
{
    if (!robotOnline) return;
    
    [[RKDeviceMessenger sharedMessenger] addDataStreamingObserver:self selector:@selector(handleDataStreaming:)];
    [RKSetDataStreamingCommand sendCommandWithSampleRateDivisor:divisor
                                                   packetFrames:frames
                                                     sensorMask:mask
                                                    packetCount:count];
}

-(void)enableControllerStreamingWithSampleRateDivisor:(uint16_t)divisor
                                         packetFrames:(uint16_t)frames
                                           sensorMask:(uint64_t)mask
 {
     if(controllerStreamingOn && !robotOnline) return;

     [RKStabilizationCommand sendCommandWithState:RKStabilizationStateOff];
     [RKBackLEDOutputCommand sendCommandWithBrightness:1.0];
     [self setDataStreamingWithSampleRateDivisor:divisor packetFrames:frames sensorMask:mask packetCount:0];
     controllerStreamingOn = YES;
    
}

-(void)disableControllerStreaming {
    if (!controllerStreamingOn) return;
    
    [[RKDeviceMessenger sharedMessenger] removeDataStreamingObserver:self];
    
    [RKSetDataStreamingCommand sendCommandWithSampleRateDivisor:0 packetFrames:0 sensorMask:0  packetCount:0];
    [RKBackLEDOutputCommand sendCommandWithBrightness:0.0];
    [RKStabilizationCommand sendCommandWithState:RKStabilizationStateOn];
    controllerStreamingOn = NO;
    
}

- (void)handleDataStreaming:(RKDeviceAsyncData *)data
{
    if ([data isKindOfClass:[RKDeviceSensorsAsyncData class]]) {
        RKDeviceSensorsAsyncData *sensors_data = (RKDeviceSensorsAsyncData *)data;
        RKDeviceSensorsData *data = [sensors_data.dataFrames objectAtIndex:0];
        
        // Send serialized object to Unity
        if (receiveDeviceMessageCallback != NULL) {
            RKDeviceMessageEncoder *encoder = [RKDeviceMessageEncoder encodeWithRootObject:sensors_data];
            receiveDeviceMessageCallback([[encoder stringRepresentation] UTF8String]);
        }
    }
}

- (void)disconnectRobots {
    [[RKRobotProvider sharedRobotProvider]closeRobotConnection];
}

extern "C" {
    
    void setupRobotConnection() {
        [[RKUNBridge sharedBridge] connectToRobot];
    }
    
    bool isRobotConnected() {
        return [[RKUNBridge sharedBridge] isRobotOnline];
    }
    
    void setRGB(float red, float green, float blue) {
        [RKRGBLEDOutputCommand sendCommandWithRed:red green:green blue:blue];
    }
    
    void roll(int heading, float speed) {
        [RKRollCommand sendCommandWithHeading:heading velocity:speed];
    }
    
    void setDataStreaming(uint16_t sampleRateDivisor, uint16_t sampleFrames,
    	 uint64_t sampleMask, uint8_t sampleCount)
    {
        [[RKUNBridge sharedBridge] setDataStreamingWithSampleRateDivisor:sampleRateDivisor
                                                            packetFrames:sampleFrames
                                                              sensorMask:sampleMask
                                                             packetCount:sampleCount];
    }
    
    void enableControllerStreaming(uint16_t divisor, uint16_t frames, uint64_t mask) {
        [[RKUNBridge sharedBridge] enableControllerStreamingWithSampleRateDivisor:divisor
                                                                     packetFrames:frames
                                                                       sensorMask:mask];
    }
    
    void disableControllerStreaming() {
        [[RKUNBridge sharedBridge] disableControllerStreaming];
    }
    
    void setHeading(int heading) {
        [RKCalibrateCommand sendCommandWithHeading:heading];
    }
    
    void setBackLED(float intensity) {
        [RKBackLEDOutputCommand sendCommandWithBrightness:intensity];
    }
    
	void _RegisterRecieveDeviceMessageCallback(ReceiveDeviceMessageCallback callback) {
        RKUNBridge *bridge = [RKUNBridge sharedBridge];
        bridge.receiveDeviceMessageCallback = callback;
    }
    
    void disconnectRobots() {
        [[RKUNBridge sharedBridge] disconnectRobots];
    }
}

@end

