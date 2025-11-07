# MyToDo - 日常任务管理应用 (DailyApp)

[![.NET Version](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE.txt)
[![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)](https://www.microsoft.com/windows)

> 一个基于WPF的现代化桌面应用，专为高效管理日常任务而设计

DailyApp 是一款优雅且实用的待办事项和备忘录管理工具，采用Material Design风格界面和MVVM架构模式。无论您是需要追踪工作任务、规划日程还是记录灵感，DailyApp 都能为您提供流畅直观的用户体验。

## 📋 目录

- [功能特点](#-功能特点)
- [技术栈](#️-技术栈)
- [项目结构](#-项目结构)
- [安装与使用](#-安装与使用)
- [贡献指南](#-贡献指南)
- [问题报告](#-问题报告)
- [支持与联系](#-支持与联系)
- [许可证](#-许可证)


## ✨ 功能特点

- **📝 待办事项管理**：轻松添加、查看、筛选待办任务，支持"全部/待办/已完成"状态智能筛选
- **📄 备忘录管理**：创建和浏览备忘录内容，采用优雅的卡片式布局展示
- **🎨 现代化UI设计**：基于Material Design设计语言，包含侧边抽屉菜单、浮动操作按钮、流畅的过渡动画
- **📱 响应式布局**：自适应不同窗口尺寸，完美支持窗口最大化、最小化等操作
- **🔍 智能搜索**：快速查找和定位待办事项及备忘录内容
- **🔐 用户认证**：支持用户登录和身份验证功能（通过API后端）


## 🛠️ 技术栈

- **前端框架**：WPF (Windows Presentation Foundation)
- **后端框架**：ASP.NET Core Web API
- **架构模式**：MVVM (通过Prism框架实现)
- **UI组件库**：MaterialDesignInXamlToolkit 4.9.0
- **依赖注入**：Prism DryIoc 容器
- **数据库**：Entity Framework Core + SQL Server
- **数据传输**：DTO (Data Transfer Object) 模式
- **HTTP客户端**：RestSharp
- **目标平台**：.NET 8.0


## 📁 项目结构

```
MyToDo/
├── DailyApp/                    # 主应用目录
│   ├── DailyApp.WPF/           # WPF客户端应用
│   │   ├── Views/              # 界面视图（XAML文件）
│   │   │   ├── MainWin.xaml    # 主窗口（导航菜单和布局框架）
│   │   │   ├── HomeUC.xaml     # 首页视图（任务和备忘录概览）
│   │   │   ├── WaitUC.xaml     # 待办事项管理界面
│   │   │   ├── MemoUC.xaml     # 备忘录管理界面
│   │   │   └── LoginWin.xaml   # 登录窗口
│   │   ├── ViewModels/         # 视图模型（业务逻辑）
│   │   │   ├── MemoUCViewModel.cs    # 备忘录视图模型
│   │   │   ├── HomeUCViewModel.cs    # 首页视图模型
│   │   │   ├── WaitUCViewModel.cs    # 待办事项视图模型
│   │   │   └── LoginWinViewModel.cs  # 登录视图模型
│   │   ├── DTOs/               # 数据传输对象
│   │   │   ├── MemoInfoDTO.cs  # 备忘录数据模型
│   │   │   ├── WaitInfoDTO.cs  # 待办事项数据模型
│   │   │   └── UserInfoDTO.cs  # 用户信息数据模型
│   │   ├── HttpClients/        # HTTP客户端和工具类
│   │   ├── Converters/         # XAML值转换器
│   │   ├── Extensions/         # 扩展方法和辅助类
│   │   └── App.xaml            # 应用入口（主题和资源配置）
│   │
│   ├── DailyApp.Api/           # ASP.NET Core Web API后端
│   │   ├── Controllers/        # API控制器
│   │   ├── DataModel/          # 数据库模型
│   │   ├── DTOs/               # 数据传输对象
│   │   ├── ApiReponses/        # API响应模型
│   │   ├── Migrations/         # 数据库迁移文件
│   │   └── Program.cs          # API应用入口
│   │
│   └── test/                   # 测试项目
│
├── README.md                    # 项目说明文档
└── LICENSE.txt                  # MIT许可证文件
```


## 🚀 安装与使用

### 环境要求

在开始之前，请确保您的开发环境满足以下要求：

- **操作系统**：Windows 10 或更高版本
- **开发框架**：.NET 8.0 SDK 或更高版本 ([下载地址](https://dotnet.microsoft.com/download))
- **开发工具**：Visual Studio 2022 或更高版本（推荐使用Community版或以上）
- **数据库**：SQL Server 2019 或更高版本（用于API后端）

### 快速开始

#### 1️⃣ 克隆仓库

```bash
git clone https://github.com/AMianSleepy/MyToDo.git
cd MyToDo
```

#### 2️⃣ 配置后端API（可选）

如果您需要使用完整的后端功能（用户认证、数据持久化等）：

1. 打开 `DailyApp/DailyApp.Api/appsettings.json` 配置数据库连接字符串
2. 使用Package Manager Console执行数据库迁移：
   ```bash
   Update-Database
   ```
3. 启动API项目（设置 `DailyApp.Api` 为启动项目）

#### 3️⃣ 运行WPF客户端

1. 使用Visual Studio打开解决方案文件：`DailyApp/DailyApp.slnx`
2. 还原NuGet包：
   - 方法1：右键点击解决方案 → 选择"还原NuGet包"
   - 方法2：在Package Manager Console中运行 `dotnet restore`
3. 设置 `DailyApp.WPF` 为启动项目
4. 按 `F5` 键或点击"启动"按钮运行应用

### 使用说明

- **首次使用**：启动应用后，如果配置了API后端，需要先注册并登录账户
- **添加待办事项**：点击右下角浮动按钮 ➕，填写任务详情
- **管理备忘录**：切换到备忘录页面，使用相同方式添加和管理备忘录
- **筛选任务**：使用顶部筛选按钮查看不同状态的待办事项
- **搜索功能**：在搜索框中输入关键词快速定位任务


## 🤝 贡献指南

我们欢迎并感谢所有形式的贡献！如果您想为 DailyApp 做出贡献，请遵循以下步骤：

### 如何贡献

1. **Fork 本仓库**到您的GitHub账户
2. **创建特性分支**：`git checkout -b feature/your-feature-name`
3. **提交您的更改**：`git commit -m '添加某某功能'`
4. **推送到分支**：`git push origin feature/your-feature-name`
5. **提交Pull Request**，详细描述您的更改

### 代码规范

- 遵循现有的代码风格和命名约定
- 为新功能添加适当的注释和文档
- 确保代码能够正常编译和运行
- 如有可能，为新功能添加单元测试

### 提交信息规范

请使用清晰、描述性的提交信息，例如：
- `feat: 添加任务优先级功能`
- `fix: 修复备忘录删除bug`
- `docs: 更新README安装说明`
- `style: 优化主题配色方案`

## 🐛 问题报告

如果您在使用过程中遇到任何问题或有功能建议，请通过以下方式反馈：

### 提交Issue

1. 前往 [Issues页面](https://github.com/AMianSleepy/MyToDo/issues)
2. 点击"New Issue"按钮
3. 选择合适的issue模板（Bug报告 或 功能请求）
4. 填写详细信息，包括：
   - **问题描述**：清晰描述遇到的问题
   - **复现步骤**：详细说明如何重现该问题
   - **期望行为**：说明您期望的正确行为
   - **环境信息**：操作系统版本、.NET版本等
   - **截图**：如适用，附上相关截图

### Bug报告模板

```markdown
**问题描述**
[简要描述问题]

**复现步骤**
1. 打开应用
2. 点击...
3. 输入...
4. 观察到错误

**期望行为**
[描述应该发生什么]

**环境信息**
- OS: [例如 Windows 11]
- .NET版本: [例如 .NET 8.0]
- 应用版本: [例如 v1.0.0]

**截图**
[如有需要，请附上截图]
```

## 📞 支持与联系

如果您需要帮助或有任何疑问，可以通过以下方式联系我们：

- **GitHub Issues**：[提交问题](https://github.com/AMianSleepy/MyToDo/issues)
- **GitHub Discussions**：[参与讨论](https://github.com/AMianSleepy/MyToDo/discussions)
- **项目维护者**：[@AMianSleepy](https://github.com/AMianSleepy)

## 📄 许可证

本项目基于 [MIT许可证](LICENSE.txt) 开源，允许自由使用、修改和分发。

---

<div align="center">

**如果觉得这个项目对您有帮助，请给个 ⭐️ Star 支持一下！**

Made with ❤️ by [AMianSleepy](https://github.com/AMianSleepy)

</div>
