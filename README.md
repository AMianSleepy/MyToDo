# MyToDo

[![.NET Version](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE.txt)
[![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)](https://www.microsoft.com/windows)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](https://github.com/AMianSleepy/MyToDo/pulls)

> **项目名称：** MyToDo  
> **应用展示名：** DailyApp  
> 一个基于 WPF 和 Material Design 的现代化日常任务管理桌面应用，用于学习实践 Prism 框架、MVVM 架构、ASP.NET Core Web API 和 Entity Framework Core

## 📖 项目简介

MyToDo 是一个学习型项目，旨在通过实战掌握以下技术栈：
- **Prism 框架**：MVVM 模式实现、依赖注入、模块化导航、对话框服务
- **WPF 现代化开发**：Material Design UI 设计、响应式布局、数据绑定
- **ASP.NET Core Web API**：RESTful API 设计、DTO 模式、控制器路由
- **Entity Framework Core**：Code First 数据模型、迁移管理、数据持久化

本项目实现了一个功能完整的日常任务管理应用（代码实现名为 DailyApp），包含待办事项管理、备忘录记录、用户登录认证、统计面板等核心功能，采用优雅的 Material Design 风格界面。

<!-- 
## 📸 应用截图

未来可在此添加应用界面截图或演示 GIF：
![主界面](docs/images/main-window.png)
![待办事项](docs/images/wait-page.png)
![备忘录](docs/images/memo-page.png)
-->

## 📋 目录

- [项目简介](#-项目简介)
- [功能特点](#-功能特点)
- [技术栈](#️-技术栈)
- [架构说明](#-架构说明)
- [项目结构](#-项目结构)
- [安装与运行](#-安装与运行)
- [登录流程说明](#-登录流程说明)
- [数据示例说明](#-数据示例说明)
- [EF 迁移说明](#-ef-迁移说明)
- [贡献指南](#-贡献指南)
- [路线规划 (Roadmap)](#-路线规划-roadmap)
- [风险与注意事项](#️-风险与注意事项)
- [问题报告](#-问题报告)
- [许可证](#-许可证)


## ✨ 功能特点

基于现有代码实现的功能模块：

### 核心功能
- **🏠 首页 (HomeUC)**：任务概览统计面板，展示汇总、已完成、完成百分比和备忘录数量，支持点击跳转到对应功能页
- **📝 待办事项管理 (WaitUC)**：
  - 添加、编辑、删除待办任务
  - 支持"全部/待办/已完成"三种状态筛选
  - 标题搜索功能
  - 任务状态快速切换
  - 对话框式添加和编辑界面 (AddWaitUC / EditWaitUC)
- **📄 备忘录管理 (MemoUC)**：
  - 创建、编辑、浏览备忘录
  - 卡片式布局展示
  - 对话框式添加和编辑界面 (AddMemoUC / EditMemoUC)
- **🔐 用户登录 (LoginUC)**：
  - 启动时对话框登录验证
  - 登录名传递到首页显示
  - 支持账号密码验证（通过 API 后端）

### UI/UX 特性
- **🎨 Material Design 界面**：基于 MaterialDesignInXamlToolkit 4.9.0 实现
- **🧭 侧边导航菜单**：左侧抽屉式菜单，支持导航到不同功能模块
- **⚙️ 设置页面 (SettingsUC)**：包含个性化设置 (PersonalUC)、关于我们 (AboutUsUC)、系统设置 (SysSetUC)
- **🔍 搜索功能**：顶部搜索框快速定位内容
- **📊 统计面板**：实时显示待办事项和备忘录的统计数据
- **📱 响应式布局**：自适应窗口大小变化


## 🛠️ 技术栈

### 前端 (DailyApp.WPF)
- **框架**：WPF (Windows Presentation Foundation)
- **目标平台**：.NET 8.0
- **MVVM 框架**：Prism 8.x (基于 DryIoc 容器)
- **UI 组件库**：MaterialDesignInXamlToolkit 4.9.0
- **依赖注入**：Prism DryIoc 容器
- **HTTP 客户端**：RestSharp
- **导航服务**：Prism IRegionManager
- **对话框服务**：Prism IDialogService + 自定义 DialogHostService

### 后端 (DailyApp.Api)
- **框架**：ASP.NET Core Web API
- **目标平台**：.NET 8.0
- **ORM**：Entity Framework Core (Code First)
- **数据库**：SQL Server
- **数据传输**：DTO (Data Transfer Object) 模式
- **对象映射**：AutoMapper

### 数据模型
- **AccountInfo**：用户账号信息（AccountId, Name, Account, Pwd）
- **WaitInfo**：待办事项信息
- **MemoInfo**：备忘录信息
- **DTO 对应类**：AccountInfoDTO, WaitInfoDTO, MemoInfoDTO, StatWaitDTO


## 🏗️ 架构说明

### 前端架构 (WPF + Prism MVVM)

#### 1. MVVM 模式实现
- **View (视图层)**：XAML 文件，定义 UI 布局和样式
- **ViewModel (视图模型层)**：继承 `BindableBase`，处理业务逻辑和数据绑定
- **Model (模型层)**：DTOs 和 Models，数据结构定义

#### 2. Prism 模块化导航
使用 `IRegionManager` 实现页面导航：
```csharp
// App.xaml.cs 中注册导航页面
containerRegistry.RegisterForNavigation<HomeUC, HomeUCViewModel>();
containerRegistry.RegisterForNavigation<WaitUC, WaitUCViewModel>();
containerRegistry.RegisterForNavigation<MemoUC, MemoUCViewModel>();
```

#### 3. 对话框服务 (Dialog Service)
- **Prism IDialogService**：用于登录对话框
- **自定义 DialogHostService**：用于添加/编辑待办和备忘录的对话框

#### 4. 依赖注入注册 (App.xaml.cs 示例)
```csharp
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    // 登录对话框
    containerRegistry.RegisterDialog<LoginUC, LoginUCViewModel>();
    
    // HTTP 客户端（注册时注入 webUrl 配置，详见 HttpRestClient）
    containerRegistry.GetContainer().Register<HttpRestClient>(
        made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
    
    // 导航页面
    containerRegistry.RegisterForNavigation<HomeUC, HomeUCViewModel>();
    containerRegistry.RegisterForNavigation<WaitUC, WaitUCViewModel>();
    containerRegistry.RegisterForNavigation<MemoUC, MemoUCViewModel>();
    
    // 自定义对话框服务
    containerRegistry.Register<DialogHostService>();
    
    // 对话框视图
    containerRegistry.RegisterForNavigation<AddWaitUC, AddWaitUCViewModel>();
    containerRegistry.RegisterForNavigation<EditWaitUC, EditWaitUCViewModel>();
    containerRegistry.RegisterForNavigation<AddMemoUC, AddMemoUCViewModel>();
    containerRegistry.RegisterForNavigation<EditMemoUC, EditMemoUCViewModel>();
}
```

**HttpRestClient 配置说明**：  
`webUrl` 参数目前在 `HttpRestClient` 构造函数中设置为硬编码的 `http://localhost:5000/api/`。  
若需修改 API 地址，可以：
1. 在 `App.xaml.cs` 中通过配置文件读取 URL
2. 使用 `containerRegistry.GetContainer().RegisterInstance<string>("webUrl", "your-api-url")`
3. 或在 `HttpRestClient` 中直接修改 `baseUrl` 字段

### 后端架构 (ASP.NET Core Web API + EF Core)

#### 1. Web API 控制器
- **AccountController**：用户账号管理（注册、登录、验证）
- **WaitController**：待办事项 CRUD 操作
- **MemoController**：备忘录 CRUD 操作

#### 2. Entity Framework Core 数据模型
- **Code First 方式**：通过 C# 类定义数据库表结构
- **DaliyDbContext**：数据库上下文，管理实体集合
- **Migrations**：数据库迁移文件，版本控制数据库架构变更

示例数据模型 (`AccountInfo.cs`)：
```csharp
[Table("AccountInfo")]
public class AccountInfo
{
    [Key]
    public int AccountId { get; set; }
    public string Name { get; set; }
    public string Account { get; set; }
    public string Pwd { get; set; }  // ⚠️ 注意：生产环境需使用哈希加密
}
```

#### 3. DTO 模式
前后端通过 DTO (Data Transfer Object) 传递数据，实现：
- 数据解耦：实体模型与传输对象分离
- 安全性：避免直接暴露数据库实体
- 灵活性：可自定义传输字段

#### 4. AutoMapper 对象映射
自动完成 Entity 与 DTO 之间的转换（参见 `AutoMapperSettingscs.cs`）

## 📁 项目结构

```
MyToDo/
├── DailyApp/                              # 主应用目录
│   ├── DailyApp.WPF/                     # WPF 客户端应用
│   │   ├── Views/                        # 界面视图（XAML 文件）
│   │   │   ├── MainWin.xaml              # 主窗口（导航菜单和布局框架）
│   │   │   ├── HomeUC.xaml               # 首页视图（统计面板和概览）
│   │   │   ├── WaitUC.xaml               # 待办事项管理界面
│   │   │   ├── MemoUC.xaml               # 备忘录管理界面
│   │   │   ├── LoginUC.xaml              # 登录对话框
│   │   │   ├── SettingsUC.xaml           # 设置页面
│   │   │   ├── PersonalUC.xaml           # 个性化设置页面
│   │   │   ├── AboutUsUC.xaml            # 关于我们页面
│   │   │   ├── SysSetUC.xaml             # 系统设置页面
│   │   │   └── Dialogs/                  # 对话框视图
│   │   │       ├── AddWaitUC.xaml        # 添加待办对话框
│   │   │       ├── EditWaitUC.xaml       # 编辑待办对话框
│   │   │       ├── AddMemoUC.xaml        # 添加备忘录对话框
│   │   │       └── EditMemoUC.xaml       # 编辑备忘录对话框
│   │   ├── ViewModels/                   # 视图模型（业务逻辑）
│   │   │   ├── MainWinViewModel.cs       # 主窗口视图模型
│   │   │   ├── HomeUCViewModel.cs        # 首页视图模型
│   │   │   ├── WaitUCViewModel.cs        # 待办事项视图模型
│   │   │   ├── MemoUCViewModel.cs        # 备忘录视图模型
│   │   │   ├── LoginUCViewModel.cs       # 登录视图模型
│   │   │   ├── SettingsUCViewModel.cs    # 设置视图模型
│   │   │   ├── PersonalUCViewModel.cs    # 个性化设置视图模型
│   │   │   └── Dialogs/                  # 对话框视图模型
│   │   │       ├── AddWaitUCViewModel.cs
│   │   │       ├── EditWaitUCViewModel.cs
│   │   │       ├── AddMemoUCViewModel.cs
│   │   │       └── EditMemoUCViewModel.cs
│   │   ├── DTOs/                         # 数据传输对象
│   │   │   ├── AccountInfoDTO.cs         # 账号信息 DTO
│   │   │   ├── WaitInfoDTO.cs            # 待办事项 DTO
│   │   │   ├── MemoInfoDTO .cs           # 备忘录 DTO
│   │   │   └── StatWaitDTO.cs            # 待办统计 DTO
│   │   ├── HttpClients/                  # HTTP 客户端和工具类
│   │   │   ├── HttpRestClient.cs         # REST API 客户端封装
│   │   │   ├── ApiRequest.cs             # API 请求模型
│   │   │   ├── ApiResponse.cs            # API 响应模型
│   │   │   └── Md5Helper.cs              # MD5 加密辅助类
│   │   ├── Service/                      # 服务层
│   │   │   ├── DialogHostService.cs      # 自定义对话框服务
│   │   │   └── IDialogHostAware.cs       # 对话框接口
│   │   ├── Models/                       # 前端数据模型
│   │   │   ├── LeftMenuInfo.cs           # 左侧菜单信息模型
│   │   │   └── StatPanelInfo.cs          # 统计面板信息模型
│   │   ├── Converters/                   # XAML 值转换器
│   │   │   └── ColorToBrushConverter.cs
│   │   ├── Extensions/                   # 扩展方法和辅助类
│   │   │   └── PasswordBoxExtend.cs      # 密码框扩展
│   │   ├── MsgEvents/                    # 消息事件
│   │   │   └── MsgEvent.cs
│   │   ├── Images/                       # 图片资源
│   │   ├── App.xaml                      # 应用入口（主题和资源配置）
│   │   ├── App.xaml.cs                   # 应用启动逻辑和依赖注入配置
│   │   └── AssemblyInfo.cs               # 程序集信息
│   │
│   ├── DailyApp.Api/                     # ASP.NET Core Web API 后端
│   │   ├── Controllers/                  # API 控制器
│   │   │   ├── AccountController.cs      # 账号管理控制器
│   │   │   ├── WaitController.cs         # 待办事项控制器
│   │   │   └── MemoController.cs         # 备忘录控制器
│   │   ├── DataModel/                    # 数据库实体模型
│   │   │   ├── DaliyDbContext.cs         # 数据库上下文
│   │   │   ├── AccountInfo.cs            # 账号实体
│   │   │   ├── WaitInfo.cs               # 待办事项实体
│   │   │   └── MemoInfo.cs               # 备忘录实体
│   │   ├── DTOs/                         # 数据传输对象
│   │   │   ├── AccountInfoDTO.cs
│   │   │   ├── WaitInfoDTO.cs
│   │   │   ├── MemoInfoDTO.cs
│   │   │   └── StatWaitDTO.cs
│   │   ├── ApiResponses/                 # API 响应模型
│   │   │   └── ApiResponse.cs
│   │   ├── Migrations/                   # EF Core 迁移文件
│   │   │   ├── 20251027075956_M.cs       # 初始迁移：创建 AccountInfo 表
│   │   │   ├── 20251107080455_WaitInfo.cs    # 添加 WaitInfo 表
│   │   │   ├── 20251115140130_MemoInfo.cs    # 添加 MemoInfo 表
│   │   │   └── DaliyDbContextModelSnapshot.cs
│   │   ├── Program.cs                    # API 应用入口
│   │   ├── AutoMapperSettingscs.cs       # AutoMapper 配置
│   │   ├── appsettings.json              # 应用配置文件
│   │   └── appsettings.Development.json  # 开发环境配置
│   │
│   └── DailyApp.slnx                     # 解决方案文件
│
├── README.md                              # 项目说明文档（本文件）
├── LICENSE.txt                            # MIT 许可证文件
└── .gitignore                             # Git 忽略文件配置
```

**注意**：目录结构中原提及的 `test/` 目录在当前代码库中不存在，未来可根据需要添加单元测试项目。


## 🚀 安装与运行

### 环境要求

- **操作系统**：Windows 10 或更高版本
- **开发框架**：.NET 8.0 SDK 或更高版本 ([下载地址](https://dotnet.microsoft.com/download))
- **开发工具**：Visual Studio 2022 或更高版本（推荐 Community 版或以上）
- **数据库**（可选）：SQL Server 2019 或更高版本（用于 API 后端数据持久化）

### 快速开始

#### 1️⃣ 克隆仓库

```bash
git clone https://github.com/AMianSleepy/MyToDo.git
cd MyToDo
```

#### 2️⃣ 运行前端 WPF 应用

1. 使用 Visual Studio 打开解决方案文件：`DailyApp/DailyApp.slnx`
2. 还原 NuGet 包：
   - 方法1：右键点击解决方案 → 选择"还原 NuGet 包"
   - 方法2：在终端中运行 `dotnet restore DailyApp/DailyApp.WPF/DailyApp.WPF.csproj`
3. 设置 `DailyApp.WPF` 为启动项目
4. 按 `F5` 键或点击"启动"按钮运行应用

**注意**：前端应用可以独立运行，当前使用静态示例数据（无需后端 API）。

#### 3️⃣ 运行后端 API（可选）

如果需要使用完整的后端功能（用户认证、数据持久化、远程同步等）：

**步骤 1：配置数据库连接字符串**

编辑 `DailyApp/DailyApp.Api/appsettings.json` 或 `appsettings.Development.json`，配置您的 SQL Server 连接字符串：

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DailyAppDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**步骤 2：应用数据库迁移**

在项目根目录或 `DailyApp/DailyApp.Api/` 目录下运行：

```bash
# 使用 dotnet CLI
dotnet ef database update --project DailyApp/DailyApp.Api

# 或在 Visual Studio Package Manager Console 中运行
Update-Database
```

这将创建数据库并应用所有迁移（AccountInfo、WaitInfo、MemoInfo 表）。

**步骤 3：启动 API 项目**

- 在 Visual Studio 中设置 `DailyApp.Api` 为启动项目，按 `F5` 运行
- 或使用命令行：
  ```bash
  dotnet run --project DailyApp/DailyApp.Api
  ```

API 默认运行在 `http://localhost:5000`（需确保前端 `HttpRestClient.cs` 中的 `baseUrl` 与此一致）。

### 使用说明

- **登录**：启动应用后会显示登录对话框，输入账号密码（如使用后端 API）或点击取消使用本地模式
- **首页概览**：查看待办事项和备忘录的统计信息，点击统计面板跳转到对应功能
- **待办事项**：
  - 点击右下角浮动按钮 ➕ 添加新任务
  - 使用顶部筛选按钮查看"全部/待办/已完成"状态
  - 点击任务卡片可编辑或删除
  - 使用搜索框按标题快速查找
- **备忘录**：切换到备忘录页面，使用类似方式添加和管理备忘录
- **设置**：访问个性化设置、系统设置和关于页面


## 🔐 登录流程说明

本项目展示了如何在 WPF 应用启动时使用 Prism 的 `IDialogService` 实现登录验证，并将登录信息传递到主页面。

### 实现流程 (App.xaml.cs)

```csharp
protected override void OnInitialized()
{
    // 1. 获取对话框服务实例
    var dialog = Container.Resolve<IDialogService>();
    
    // 2. 显示登录对话框
    dialog.ShowDialog("LoginUC", callback =>
    {
        // 3. 检查用户是否成功登录
        if (callback.Result != ButtonResult.OK)
        {
            // 用户取消登录，退出应用
            Environment.Exit(0);
            return;
        }
        
        // 4. 获取主窗口 ViewModel
        var mainVM = Current.MainWindow.DataContext as MainWinViewModel;
        if (mainVM != null)
        {
            // 5. 从对话框参数中获取登录名
            if (callback.Parameters.ContainsKey("LoginName"))
            {
                string name = callback.Parameters.GetValue<string>("LoginName");
                
                // 6. 将登录名传递到主窗口，设置默认导航页
                mainVM.SetDefaultNav(name);
            }
        }
        
        // 7. 调用基类初始化方法，显示主窗口
        base.OnInitialized();
    });
}
```

### 关键点

- **对话框注册**：在 `RegisterTypes` 中注册 `LoginUC` 和其 ViewModel
- **参数传递**：通过 `IDialogParameters` 在对话框和主窗口之间传递数据
- **强制登录**：如果用户未成功登录 (ButtonResult != OK)，应用将退出
- **数据流向**：LoginUC → App.xaml.cs → MainWinViewModel → HomeUCViewModel

## 📊 数据示例说明

### 当前数据来源

**前端 (DailyApp.WPF)**：
- 目前 `WaitUCViewModel` 和 `MemoUCViewModel` 使用**静态示例数据**进行开发和调试
- 数据在应用关闭后不会保存
- 适合用于学习 MVVM 数据绑定和 UI 交互

**后端 (DailyApp.Api)**：
- 提供完整的 RESTful API 接口
- 通过 Entity Framework Core 实现数据持久化到 SQL Server
- 支持真实的 CRUD 操作

### 未来规划

- ✅ **已实现**：后端 API 和数据库模型
- 🚧 **进行中**：前端与后端 API 的完整集成
- 📋 **计划中**：
  - 本地缓存机制（SQLite）
  - 离线模式支持
  - 数据同步策略（本地 ↔ 远程）

## 🗄️ EF 迁移说明

本项目使用 **Entity Framework Core Code First** 方式管理数据库架构。

### 现有迁移

| 迁移名称 | 创建日期 | 作用 |
|---------|---------|------|
| `20251027075956_M` | 2025-10-27 | 初始迁移：创建 `AccountInfo` 表（用户账号） |
| `20251107080455_WaitInfo` | 2025-11-07 | 添加 `WaitInfo` 表（待办事项） |
| `20251115140130_MemoInfo` | 2025-11-15 | 添加 `MemoInfo` 表（备忘录） |

### 如何添加新迁移

当您修改数据模型（添加新表、添加/删除字段等）后：

```bash
# 在 DailyApp.Api 项目目录下
cd DailyApp/DailyApp.Api

# 添加新迁移（替换 YourMigrationName 为描述性名称）
dotnet ef migrations add YourMigrationName

# 应用迁移到数据库
dotnet ef database update
```

或在 Visual Studio Package Manager Console 中：

```powershell
# 设置默认项目为 DailyApp.Api
Add-Migration YourMigrationName
Update-Database
```

### 重要提示

⚠️ **安全警告**：当前 `AccountInfo.Pwd` 字段存储**明文密码**，仅用于学习演示。  
**生产环境必须**：
- 使用密码哈希算法（如 BCrypt、PBKDF2、Argon2）
- 添加密码盐值 (Salt)
- 实现安全的身份验证机制（如 ASP.NET Core Identity）

## 🤝 贡献指南

我们欢迎并感谢所有形式的贡献！如果您想为 MyToDo 做出贡献，请遵循以下步骤：

### 贡献流程

1. **查看 Issues**：浏览 [Issues 页面](https://github.com/AMianSleepy/MyToDo/issues) 查找感兴趣的问题或提出新建议
2. **Fork 仓库**：点击右上角 Fork 按钮，将仓库 Fork 到您的 GitHub 账户
3. **克隆到本地**：
   ```bash
   git clone https://github.com/YOUR_USERNAME/MyToDo.git
   cd MyToDo
   ```
4. **创建特性分支**：
   ```bash
   git checkout -b feature/your-feature-name
   # 或
   git checkout -b fix/bug-description
   ```
5. **进行开发**：
   - 遵循现有代码风格和命名约定
   - 为新功能添加适当的注释
   - 确保代码能够正常编译和运行
6. **提交更改**：
   ```bash
   git add .
   git commit -m "feat: 添加某某功能"
   ```
7. **推送到您的 Fork**：
   ```bash
   git push origin feature/your-feature-name
   ```
8. **提交 Pull Request**：
   - 访问原仓库的 Pull Requests 页面
   - 点击 "New Pull Request"
   - 详细描述您的更改内容和目的
   - 链接相关的 Issue（如适用）

### 代码规范

- 遵循 C# 编码规范和命名约定
- 使用有意义的变量和方法名
- 为复杂逻辑添加注释
- 保持代码简洁可读
- 如有可能，为新功能添加单元测试

### 提交信息规范

请使用清晰、描述性的提交信息，建议遵循 [Conventional Commits](https://www.conventionalcommits.org/) 规范：

- `feat: 添加任务优先级功能`
- `fix: 修复备忘录删除时的空引用异常`
- `docs: 更新 README 安装说明`
- `style: 优化主题配色方案`
- `refactor: 重构 HttpRestClient 类`
- `test: 添加 WaitUCViewModel 单元测试`
- `chore: 更新 NuGet 包版本`

## 🗺️ 路线规划 (Roadmap)

### 近期计划

- [ ] **前后端集成**：完成前端与后端 API 的完整对接
- [ ] **真实 CRUD 操作**：将静态示例数据替换为 API 调用
- [ ] **身份验证增强**：实现 JWT Token 或 ASP.NET Core Identity
- [ ] **密码安全**：实现密码哈希和盐值存储

### 中期计划

- [ ] **本地缓存**：使用 SQLite 实现离线数据存储
- [ ] **数据同步**：实现本地数据与远程服务器的同步机制
- [ ] **任务提醒**：添加任务到期提醒功能
- [ ] **任务优先级**：支持设置任务的优先级（高/中/低）
- [ ] **任务分类**：支持自定义任务分类和标签

### 长期计划

- [ ] **多语言支持**：国际化 (i18n) 实现中英文切换
- [ ] **主题切换**：支持亮色/暗色主题
- [ ] **数据导入导出**：支持导出为 JSON/CSV 格式
- [ ] **单元测试**：为核心业务逻辑添加单元测试
- [ ] **CI/CD**：配置 GitHub Actions 自动化构建和测试
- [ ] **安装包**：提供 MSI 或 MSIX 安装包
- [ ] **云端同步**：支持多设备数据同步（可选 Azure/AWS 后端）

### 功能改进

- [ ] **搜索增强**：支持全文搜索和高级筛选
- [ ] **统计报表**：可视化任务完成趋势图表
- [ ] **附件支持**：为待办事项和备忘录添加附件功能
- [ ] **协作功能**：支持多用户协作和任务分配
- [ ] **移动端**：开发 Xamarin.Forms 或 MAUI 移动版本

## ⚠️ 风险与注意事项

### 当前限制

1. **示例数据**：
   - 前端当前使用静态示例数据，未完全集成后端 API
   - 数据不会持久化保存，应用重启后会丢失
   - **不适合生产环境使用**

2. **登录流程**：
   - 当前登录为演示流程，未实现完整的身份验证
   - 密码以明文形式存储（仅用于学习）
   - **生产环境必须实现密码哈希和安全认证**

3. **API 安全**：
   - API 未实现身份验证和授权机制
   - 所有接口均为公开访问
   - **生产环境必须添加 JWT/OAuth2 认证**

### 安全建议

如果您计划将此项目用于实际生产环境，**必须**实施以下安全措施：

✅ **密码安全**：
- 使用 BCrypt、PBKDF2 或 Argon2 进行密码哈希
- 为每个密码生成唯一的盐值
- 永远不要以明文形式存储密码

✅ **身份验证**：
- 实现 JWT Token 或 Cookie 身份验证
- 使用 ASP.NET Core Identity 管理用户
- 实现登录失败次数限制和账号锁定

✅ **授权控制**：
- 为 API 接口添加授权验证
- 实现基于角色的访问控制 (RBAC)
- 验证用户只能访问自己的数据

✅ **数据保护**：
- 使用 HTTPS 加密传输数据
- 配置 CORS 策略限制跨域访问
- 实施 SQL 注入防护
- 验证和清理用户输入

✅ **其他建议**：
- 定期更新 NuGet 包以修复安全漏洞
- 实施日志记录和监控
- 配置数据备份策略

### 学习目的声明

本项目主要用于**学习和演示**以下技术：
- WPF 和 Prism 框架的使用
- MVVM 架构模式的实现
- Entity Framework Core 的应用
- ASP.NET Core Web API 开发

**请勿直接将此项目用于生产环境，除非实施了上述安全措施。**

## 🐛 问题报告

如果您在使用过程中遇到任何问题或有功能建议，请通过以下方式反馈：

### 提交 Issue

1. 前往 [Issues 页面](https://github.com/AMianSleepy/MyToDo/issues)
2. 点击 "New Issue" 按钮
3. 选择合适的模板：
   - 🐛 **Bug Report**：报告程序错误或异常
   - 💡 **Feature Request**：提出新功能建议
   - 📝 **Documentation**：文档改进建议
   - ❓ **Question**：使用疑问或技术咨询
4. 填写详细信息

### Bug 报告模板

```markdown
### 问题描述
[清晰简要地描述遇到的问题]

### 复现步骤
1. 启动应用
2. 点击 "..."
3. 输入 "..."
4. 观察到错误：...

### 期望行为
[描述您期望应该发生什么]

### 实际行为
[描述实际发生了什么]

### 环境信息
- **操作系统**：Windows 11 / Windows 10
- **.NET 版本**：.NET 8.0.x
- **Visual Studio 版本**：2022 Community / Professional
- **应用版本/分支**：master / develop / v1.0.0

### 截图或日志
[如适用，请附上错误截图或日志信息]

### 其他信息
[任何可能有助于解决问题的额外信息]
```

### 功能请求模板

```markdown
### 功能描述
[清晰描述您希望添加的功能]

### 使用场景
[说明为什么需要这个功能，它解决什么问题]

### 建议实现方式
[如有想法，可以描述如何实现这个功能]

### 替代方案
[是否有其他可行的替代方案]
```

## 📞 支持与联系

如果您需要帮助或有任何疑问，可以通过以下方式联系：

- **GitHub Issues**：[提交问题或建议](https://github.com/AMianSleepy/MyToDo/issues)
- **GitHub Discussions**：[参与社区讨论](https://github.com/AMianSleepy/MyToDo/discussions)
- **Pull Requests**：[贡献代码](https://github.com/AMianSleepy/MyToDo/pulls)
- **项目维护者**：[@AMianSleepy](https://github.com/AMianSleepy)

---

## 📦 Release Notes

<!-- 占位符：未来发布版本时，在此添加版本发布说明链接 -->
<!-- 示例：查看 [发布历史](https://github.com/AMianSleepy/MyToDo/releases) 了解各版本更新内容 -->

**当前状态**：项目处于开发阶段，暂未发布正式版本。

---

## 🎓 学习资源

如果您想深入学习本项目使用的技术栈，推荐以下资源：

### WPF & MVVM
- [Microsoft WPF 官方文档](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
- [Prism Library 官方文档](https://prismlibrary.com/docs/)
- [Material Design In XAML Toolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)

### ASP.NET Core & EF Core
- [ASP.NET Core 官方文档](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core 官方文档](https://docs.microsoft.com/en-us/ef/core/)
- [RESTful API 设计指南](https://restfulapi.net/)

### 设计模式
- [MVVM 模式详解](https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern)
- [依赖注入 (DI) 模式](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [DTO 模式最佳实践](https://martinfowler.com/eaaCatalog/dataTransferObject.html)

## 📄 许可证

本项目基于 [MIT 许可证](LICENSE.txt) 开源，允许自由使用、修改和分发。

```
MIT License

Copyright (c) [year] [fullname]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software...
```

**注意**：LICENSE.txt 文件中的 `[year]` 和 `[fullname]` 为占位符，建议项目维护者填写实际的版权年份和姓名。例如：
- `[year]` → `2025`
- `[fullname]` → `AMianSleepy` 或您的真实姓名

---

## 🙏 致谢

感谢以下开源项目和社区：

- [Prism Library](https://github.com/PrismLibrary/Prism) - MVVM 框架支持
- [MaterialDesignInXamlToolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit) - Material Design UI 组件
- [RestSharp](https://github.com/restsharp/RestSharp) - HTTP 客户端库
- [Entity Framework Core](https://github.com/dotnet/efcore) - ORM 框架
- [AutoMapper](https://github.com/AutoMapper/AutoMapper) - 对象映射库

---

<div align="center">

### ⭐ 如果觉得这个项目对您有帮助，请给个 Star 支持一下！

**Made with ❤️ for Learning**

[🏠 首页](https://github.com/AMianSleepy/MyToDo) · 
[📝 提交 Issue](https://github.com/AMianSleepy/MyToDo/issues) · 
[🔀 提交 PR](https://github.com/AMianSleepy/MyToDo/pulls) · 
[@AMianSleepy](https://github.com/AMianSleepy)

---

**Happy Coding! 🚀**

</div>
