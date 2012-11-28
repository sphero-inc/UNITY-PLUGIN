#import <UIKit/UIKit.h>

@interface AppController : NSObject<UIAccelerometerDelegate, UIApplicationDelegate>
{
}
- (void) startUnity:(UIApplication*)application;
- (void) startRendering;
- (void) Repaint;
- (void) RepaintDisplayLink;
- (void) prepareRunLoop;
@end

#define NSTIMER_BASED_LOOP 0
#define THREAD_BASED_LOOP 1
#define EVENT_PUMP_BASED_LOOP 2
