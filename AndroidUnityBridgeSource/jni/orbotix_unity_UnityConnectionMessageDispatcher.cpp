#include <orbotix_unity_UnityConnectionMessageDispatcher.h>
//#include <string>
#include <jni.h>
#include <stdlib.h>
#include <android/log.h>

#ifdef __cplusplus
extern "C" {
#endif

static JavaVM *cachedJVM = NULL;

JNIEnv* GetEnv();
jclass GetUnityConnectionMessageDispatcherClass();
void logd(const char*);

JNIEXPORT jint JNICALL JNI_OnLoad(JavaVM *jvm, void *reserved){
    JNIEnv *env;
    cachedJVM = jvm;
    if((jvm)->GetEnv((void**)&env, JNI_VERSION_1_6)){
        logd("Could not find *JNIEnv");
        return JNI_ERR;
    }
    return JNI_VERSION_1_6;
}

JNIEnv* GetEnv(){
    JNIEnv* env;
    cachedJVM->AttachCurrentThread(&env, NULL);
    return env;
}

jclass GetUnityConnectionMessageDispatcherClass() {
    JNIEnv* env = GetEnv();
    return env->FindClass("orbotix/unity/UnityConnectionMessageDispatcher");
}

/*
 * Class:     orbotix_unity_UnityConnectionMessageDispatcher
 * Method:    testCallback
 * Signature: ()V
 */
JNIEXPORT void JNICALL Java_orbotix_unity_UnityConnectionMessageDispatcher_handleDataStreaming
  (JNIEnv *env, jobject object, jstring msg)
  {
        // Send serialized object to Unity
        if (unityMessageCallback != NULL) {
           // RKDeviceMessageEncoder *encoder = [RKDeviceMessageEncoder encodeWithRootObject:sensors_data];
            //receiveDeviceMessageCallback([[encoder stringRepresentation] UTF8String]);
            unityMessageCallback(env->GetStringUTFChars(msg, 0));
        }
  }

  void _RegisterRecieveDeviceMessageCallback(ReceiveDeviceMessageCallback callback) {
      unityMessageCallback = callback;
  }

  void logd(const char* msg)
  {
      //cout works in iOS to
      //cout << "RKAchievementManager:: " << msg << endl;
      __android_log_print(ANDROID_LOG_DEBUG, "unity_bridge", msg);
  }

///*
// * Class:     orbotix_unity_UnityConnectionMessageDispatcher
// * Method:    recordAchievementEvent
// * Signature: (Ljava/lang/String;I)V
// */
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_recordAchievementEvent
//  (JNIEnv *env, jobject obj, jstring eventName, jint count) {
//     RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//     manager->recordEvent(env->GetStringUTFChars(eventName, 0), count);
//  }
//
///*
// * Class:     orbotix_unity_UnityConnectionMessageDispatcher
// * Method:    getAchievementEventCount
// * Signature: (Ljava/lang/String;)I
// */
//JNIEXPORT jint JNICALL Java_orbotix_achievement_AchievementManager_getAchievementEventCount
//  (JNIEnv *env, jobject obj, jstring eventName) {
//     RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//     return manager->getEventCount(env->GetStringUTFChars(eventName, 0));
//  }
//
///*
// * Class:     orbotix_unity_UnityConnectionMessageDispatcher
// * Method:    reportWSCallLoaded
// * Signature: (Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V
// */
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_reportWSCallLoaded
//  (JNIEnv *env, jobject obj, jstring location, jstring payload, jstring response, jstring identifier, jint responseCode) {
//    RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//    manager->wsCallLoaded(std::string(env->GetStringUTFChars(location, 0)), std::string(env->GetStringUTFChars(payload, 0)), std::string(env->GetStringUTFChars(response, 0)), std::string(env->GetStringUTFChars(identifier, 0)), responseCode);
//  }
//
///*
// * Class:     orbotix_unity_UnityConnectionMessageDispatcher
// * Method:    recordColorChange
// * Signature: (FFF)V
// */
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_recordColorChange
//  (JNIEnv *env, jobject obj, jstring json) {
//    if(!setup) return;
//    RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//    manager->recordBallEvent("colorChanges", env->GetStringUTFChars(json, 0));
//  }
//
///*
// * Class:     orbotix_unity_UnityConnectionMessageDispatcher
// * Method:    recordBoost
// * Signature: (F)V
// */
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_recordBoost
//  (JNIEnv *env, jobject obj, jstring json) {
//    if(!setup) return;
//    RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//    manager->recordBallEvent("boosts", env->GetStringUTFChars(json, 0));
//  }
//
///*
// * Class:     orbotix_unity_UnityConnectionMessageDispatcher
// * Method:    recordDriveTime
// * Signature: (F)V
// */
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_recordDriveTime
//  (JNIEnv *env, jobject obj, jstring json) {
//    if(!setup) return;
//    RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//    manager->recordBallEvent("driveTimes", env->GetStringUTFChars(json, 0));
//  }
//
///*
// * Class:     orbotix_unity_UnityConnectionMessageDispatcher
// * Method:    recordDriveDistance
// * Signature: (F)V
// */
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_recordDriveDistance
//  (JNIEnv *env, jobject obj, jstring json) {
//    if(!setup) return;
//    RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//    manager->recordBallEvent("distances", env->GetStringUTFChars(json, 0));
//  }
//
///*
// * Class:     orbotix_unity_UnityConnectionMessageDispatcher
// * Method:    recordCollision
// * Signature: (F)V
// */
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_recordCollision
//  (JNIEnv *env, jobject obj, jstring json) {
//    if(!setup) return;
//    RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//    manager->recordBallEvent("crashes", env->GetStringUTFChars(json, 0));
//  }
//
///*
// * Class:     orbotix_unity_UnityConnectionMessageDispatcher
// * Method:    recordMacro
// * Signature: (F)V
// */
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_recordMacro
//  (JNIEnv *env, jobject obj, jstring json) {
//    if(!setup) return;
//    RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//    manager->recordBallEvent("macroUses", env->GetStringUTFChars(json, 0));
//  }
//
//
///*
// * Class:     orbotix_unity_UnityConnectionMessageDispatcher
// * Method:    setRobotAddress
// * Signature: (Ljava/lang/String;Ljava/lang/String;)V
// */
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_setRobotAddress
//  (JNIEnv *env, jobject obj, jstring address, jstring name) {
//    if(!setup) return;
//    RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//    manager->setBall(env->GetStringUTFChars(address, 0), env->GetStringUTFChars(name, 0));
//  }
//
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_setOAuth
//  (JNIEnv *env, jobject obj, jstring oauth) {
//    RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//    manager->loginUser(env->GetStringUTFChars(oauth, 0));
//  }
//
//JNIEXPORT void JNICALL Java_orbotix_achievement_AchievementManager_updateRobotName
//  (JNIEnv *, jobject) {
//    RKAchievementManager *manager = &RKAchievementManager::sharedManager(NULL);
//    manager->getBallName();
//  }

#ifdef __cplusplus
}
#endif
