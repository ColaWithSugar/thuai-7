# 快速入门指南

这份快速入门指南将帮助您迅速开始编写比赛程序。

## 1. 下载前端、后端和 SDK 

- **前端**：提供图形化界面，展示回放中的比赛情况。
- **后端**：作为服务器，控制比赛进行，并生成比赛回放。
- **SDK**：提供编写比赛程序所需的接口。

请从以下链接下载最新版本的前端（client）和后端（server），务必根据您的操作系统版本选择适当的版本：

[https://github.com/thuasta/thuai-7/releases](https://github.com/thuasta/thuai-7/releases)

下载完成后，解压文件至您的工作目录。

此外，请克隆或下载我们的 SDK 仓库，以便开始编写比赛程序。

克隆 C++ SDK 仓库：

```bash
git clone https://github.com/thuasta/thuai-7-agent-template-cpp.git
```

克隆 Python SDK 仓库：

```bash
git clone https://github.com/thuasta/thuai-7-agent-template-python.git
```

请持续关注我们的发布版本和群聊通知，以获取最新版本信息。

## 2. 编写比赛程序

选手需要使用提供的 SDK 编写比赛程序。详细信息请参考 [Python SDK 文档](python_sdk.md) 或 [C++ SDK 文档](C++_sdk.md)。

## 3. 启动后端和 SDK 程序

编写完成后，请先后启动后端和 SDK 程序，开始比赛。后端默认需要 2 个玩家，因此在启动后端后，您需要同时启动两个 SDK 程序。注意，两个 SDK 程序的 token 必须不同，您可以在命令行中指定 token，也可以在代码中指定。

在比赛的任何阶段，您都可以在后端输入 "stop" 结束比赛。只有在选手输入 "stop" 后，才会在后端根目录下生成一个 *.dat 回放文件。

## 4. 使用前端查看回放文件

将生成的回放文件放置在前端的 Records 文件夹中，然后打开前端即可查看回放。具体操作请参考[前端使用指南](viewer.md)。您也可以用解压软件将 *.dat 文件解压，然后查看其中的 JSON 文件，以了解比赛的详细信息。

## 5. 提交代码

请将您的代码提交到我们的比赛服务器，以便与其他选手进行对战。可在服务器上查看天梯比赛结果。

详细步骤待续...

这份指南旨在为您提供具体、清晰的指引，以确保您能够轻松开始编写比赛程序。