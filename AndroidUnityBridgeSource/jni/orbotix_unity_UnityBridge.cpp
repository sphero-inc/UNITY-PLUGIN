#include <orbotix_unity_UnityBridge.h>
//#include <string>
#include <jni.h>
#include <stdlib.h>
#include <android/log.h>

#ifdef __cplusplus
extern "C" {
#endif

static JavaVM *cachedJVM = NULL;

JNIEnv* GetEnv();
jclass GetUnityBridgeClass();
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

jclass GetUnityBridgeClass() {
    JNIEnv* env = GetEnv();
    return env->FindClass("orbotix/unity/UnityBridge");
}
  
/*
 * Class:     orbotix_unity_UnityBridge
 * Method:    sendMessage
 * Signature: ()V
 */
JNIEXPORT void JNICALL Java_orbotix_unity_UnityBridge_sendMessage
  (JNIEnv *env, jobject object, jstring msg)
  {
        // Send serialized object to Unity
        if (unityMessageCallback != NULL) {
            unityMessageCallback(env->GetStringUTFChars(msg, 0));
        }
  }

/*
 * Class:     orbotix_unity_UnityBridge
 * Method:    _RegisterRecieveDeviceMessageCallback
 * Signature: ()V
 */
  void _RegisterRecieveDeviceMessageCallback(ReceiveDeviceMessageCallback callback) {
      unityMessageCallback = callback;
  }

  void logd(const char* msg)
  {
      __android_log_print(ANDROID_LOG_DEBUG, "unity_bridge", msg);
  }

#ifdef __cplusplus
}
#endif
