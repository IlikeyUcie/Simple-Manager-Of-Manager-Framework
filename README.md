# Simple-Manager-Of-Manager-Framework
### 这是一个新手构建的第一个以Manager of Manager 为基础的框架
### 这个仓库将构建好大体的Manager of Manager框架，并且实现GameManager以及其他子Manager的基本功能
### 该框架的主要参考为贪睡的半只狗提供的基础框架，链接为https://github.com/HalfADog/Unity-ARPGGameDemo-TheDawnAbyss.git
### 在上面提供的基础框架下，使用Grok 4.1学习进行了些许优化,我在这只是记录该框架的学习过程
***
***
### 接下来是日志更新
### 2025/12/7
 这一次更新是构建好了基本的MonoSingle泛型单例类，创建好了并理解了该类后，就可以使用泛型来构建任何想要进行单例化的类。
### 2025/12/8
- 更新了GameManager，使其成为了所有Manager的入口。
- 在GameManager中创建了各Manager的字段，并且设置了初始化逻辑。
- 增加了退出游戏的方法。
- 发现Manager内部的子Manager并不是单例模式，虽然继承GameManager可以保证唯一入口且可随时调用。但无法保证唯一实例。因此改正为MonoSingleton继承。
- 发现GameManager中的子Manager初始化赋值有问题，重新改正了。
### 2025/12/10
- 修改了MonoSingleton中没有使用static导致继承无单例的bug
- 添加了AssetManager用来添加和管理资源
### 2025/12/12
- 添加了AudioManager，并制作了其基本的功能模块
- 添加了GameSoundGroupDataSO作为音频资源的数据容器
### 2025/12/14
- 学习了Cinemahine，添加了CameraManager，并制做了其基本的功能模块
### 2025/12/16
- 学习了InputSystem，添加了一个InputTools用来添加和取消订阅事件函数
### 2025/12/18
- 继续学习了InputSystem，添加了InputManager中的基本功能，其中包括，以委托的形式将回调函数订阅至事件中，在每帧结束前重置InputStateData需要重置的状态。
- 添加了InputStateData，其中都是在输入后需要改变的状态，这个类主要是用来检测输入状态的。
- 在InputTools中添加了一个切换ActionMap的扩展方法。
### 2025/12/20
- 学习了UniTask的相关内容，了解了基本知识，并且创建了一个基于UniTask延迟的TimeScaleManager
### 2025/12/22
- 添加了事件管理器，其中包含，事件的注册，事件的注销，事件的广播方法。
- 添加了事件类型脚本，其中包含1-3种参数类型的三个泛型事件类。单个泛型类中的内容包括，初始委托，传入参数则invoke委托，为委托注册回调函数，为委托注销回调函数。最后三种泛型类继承一个接口实现事件的多态管理。
- 添加了事件参数类型脚本，通过创建三个1-3种类型的泛型参数类来打包参数类型，作为参数包传入事件中被调用。最后通过继承统一接口实现多态。
### 2025/12/26
-添加了UI管理器，其中包含，面板资源的异步加载，面板的获取，面板从场景中获取，面板从场景中注册到缓存，面板的展示，面板的隐藏等方法。
-添加了BasePanle作为所有面板的基类，在该类中实现了，面板的渐变式显示以及隐藏功能。
-添加了MainCanvas作为所有面板的父节点，在该类中实现了，转换场景的渐出式效果。
### 至此，基本的几个Manager全部都已经实现，我会进行一段时间的停止更新，因为需要筹备Global Game Jam，我需要去学习快速开发的知识。
### 在Global Game Jam结束后，我还会更新其他的相对进阶的Manager来补充好这个框架。
