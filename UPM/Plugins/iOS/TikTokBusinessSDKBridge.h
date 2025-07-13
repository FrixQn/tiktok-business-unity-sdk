//
//  TiktokBusinessSDKBridge.hpp
//  Unity-iPhone
//
//  Created by ByteDance on 2024/4/15.
//

extern "C"{
void InitializeSdkFromUnityInvoke();
void IdentifyFromUnityInvoke(const char * externalId ,const char * externalUserName,const char * phoneNumber,const char * email);
void LogoutFromUnityInvoke();
}
