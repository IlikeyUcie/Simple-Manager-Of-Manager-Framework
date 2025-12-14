# Simple-Manager-Of-Manager-Framework
### 这是一个新手构建的第一个以Manager of Manager 为基础的框架
### 这个仓库将构建好大体的Manager of Manager框架，并且实现GameManager以及其他子Manager的基本功能
### 该框架的构建思路以及具体实现来源于Grok 4.1 ,我在这只是记录我的学习过程
***
***
### 接下来是日志更新
### 2025/12/7
 这一次更新是构建好了基本的MonoSingle泛型单例类，创建好了并理解了该类后，就可以使用泛型来构建任何想要进行单例化的类。
### 2025/12/8
中午进行了一次更新
- 更新了GameManager，使其成为了所有Manager的入口。
- 在GameManager中创建了各Manager的字段，并且设置了初始化逻辑。
- 增加了退出游戏的方法。
  
晚上进行了更新，主要是修复存在的问题
- 发现Manager内部的子Manager并不是单例模式，虽然继承GameManager可以保证唯一入口且可随时调用。但无法保证唯一实例。因此改正为MonoSingleton继承。
- 发现GameManager中的子Manager初始化赋值有问题，重新改正了。
### 2025/12/10
下午进行了更新
- 修改了MonoSingleton中没有使用static导致继承无单例的bug
- 添加了AssetManager用来添加和管理资源
### 2025/12/12
晚上进行了更新
- 添加了AudioManager，并制作了其基本的功能模块
- 添加了GameSoundGroupDataSO作为音频资源的数据容器
### 2025/12/14
晚上进行了更新
- 学习了Cinemahine，添加了CameraManager，并制做了其基本的功能模块
