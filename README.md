# MyToDo
# 日常任务管理应用 (DailyApp)

一个基于WPF的桌面应用，用于管理待办事项和备忘录，采用现代化UI设计和MVVM架构，帮助用户高效规划日常任务。


## 项目介绍

DailyApp 是一款简洁实用的日常任务管理工具，主要功能包括待办事项跟踪、备忘录管理，支持添加、查看、筛选等操作，界面采用Material Design风格，提供直观友好的用户体验。


## 功能特点

- **待办事项管理**：添加、查看、筛选待办任务（支持"全部/待办/已完成"状态筛选）
- **备忘录管理**：创建、浏览备忘录内容，以卡片式布局展示
- **现代化UI**：基于Material Design风格，包含抽屉菜单、浮动按钮、过渡动画等元素
- **响应式布局**：适配不同窗口大小，支持窗口最大化、最小化操作
- **搜索功能**：快速查找待办事项和备忘录


## 技术栈

- **框架**：WPF (Windows Presentation Foundation)
- **架构模式**：MVVM (通过Prism框架实现)
- **UI组件库**：MaterialDesignInXamlToolkit
- **依赖注入**：Prism内置容器
- **数据传输**：使用DTO (Data Transfer Object) 模式封装数据


## 项目结构

```
DailyApp.WPF/
├─ Views/             # 界面视图（XAML文件）
│  ├─ MainWin.xaml    # 主窗口（包含导航菜单和布局框架）
│  ├─ HomeUC.xaml     # 首页（整合待办和备忘录概览）
│  ├─ WaitUC.xaml     # 待办事项管理界面
│  ├─ MemoUC.xaml     # 备忘录管理界面
│  └─ 其他辅助视图...
├─ ViewModels/        # 视图模型（实现业务逻辑）
│  ├─ MemoUCViewModel.cs  # 备忘录视图模型
│  ├─ HomeUCViewModel.cs  # 首页视图模型
│  └─ ...
├─ DTOs/              # 数据传输对象
│  ├─ MemoInfoDTO.cs  # 备忘录数据模型
│  ├─ WaitInfoDTO.cs  # 待办事项数据模型
│  └─ ...
├─ HttpClients/       # 网络请求工具（包含MD5加密工具）
└─ App.xaml           # 应用入口配置（主题、资源字典）
```


## 安装与使用

1. **环境要求**：
   - .NET Framework 4.7.2 及以上
   - Visual Studio 2019+（推荐）

2. **运行步骤**：
   - 克隆本仓库到本地
   - 使用Visual Studio打开解决方案 `MyToDo.sln`
   - 还原NuGet包（右键解决方案 -> "还原NuGet包"）
   - 启动项目（F5）


## 许可证

本项目基于 [MIT许可证](LICENSE.txt) 开源，允许自由使用、修改和分发。这条消息已经在编辑器中准备就绪。你想如何调整这篇文档?请随时告诉我。
