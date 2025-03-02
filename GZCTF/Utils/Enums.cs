﻿using System.Text.Json.Serialization;

namespace CTFServer;

/// <summary>
/// 用户权限枚举
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role : byte
{
    /// <summary>
    /// 小黑屋用户权限
    /// </summary>
    Banned = 0,

    /// <summary>
    /// 常规用户权限
    /// </summary>
    User = 1,

    /// <summary>
    /// 监控者权限，可查询提交日志
    /// </summary>
    Monitor = 2,

    /// <summary>
    /// 管理员权限，可查看系统日志
    /// </summary>
    Admin = 3,
}

/// <summary>
/// 登录响应状态
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RegisterStatus : byte
{
    /// <summary>
    /// 注册成功已经登录
    /// </summary>
    LoggedIn = 0,

    /// <summary>
    /// 等待管理员确认
    /// </summary>
    AdminConfirmationRequired = 1,

    /// <summary>
    /// 等待邮箱确认
    /// </summary>
    EmailConfirmationRequired = 2
}

/// <summary>
/// 任务执行状态
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaskStatus : sbyte
{
    /// <summary>
    /// 任务正在进行
    /// </summary>
    Pending = -1,

    /// <summary>
    /// 任务成功完成
    /// </summary>
    Success = 0,

    /// <summary>
    /// 任务执行失败
    /// </summary>
    Fail = 1,

    /// <summary>
    /// 任务遇到重复错误
    /// </summary>
    Duplicate = 2,

    /// <summary>
    /// 任务处理被拒绝
    /// </summary>
    Denied = 3,

    /// <summary>
    /// 任务请求未找到
    /// </summary>
    NotFound = 4,

    /// <summary>
    /// 任务线程将要退出
    /// </summary>
    Exit = 5,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FileType : byte
{
    /// <summary>
    /// 无附件
    /// 正常情况下 Attachment 不会是此值，若无附件则 Attachment 为 null
    /// </summary>
    None = 0,

    /// <summary>
    /// 本地文件
    /// </summary>
    Local = 1,

    /// <summary>
    /// 远程文件
    /// </summary>
    Remote = 2,
}

/// <summary>
/// 容器状态
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContainerStatus : byte
{
    /// <summary>
    /// 正在启动
    /// </summary>
    Pending = 0,

    /// <summary>
    /// 正在运行
    /// </summary>
    Running = 1,

    /// <summary>
    /// 已销毁
    /// </summary>
    Destroyed = 2
}

/// <summary>
/// 比赛公告类型
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NoticeType : byte
{
    /// <summary>
    /// 常规公告
    /// </summary>
    Normal = 0,

    /// <summary>
    /// 一血公告
    /// </summary>
    FirstBlood = 1,

    /// <summary>
    /// 二血公告
    /// </summary>
    SecondBlood = 2,

    /// <summary>
    /// 三血公告
    /// </summary>
    ThirdBlood = 3,

    /// <summary>
    /// 发布新的提示
    /// </summary>
    NewHint = 4,

    /// <summary>
    /// 发布新的题目
    /// </summary>
    NewChallenge = 5,
}

public static class SubmissionTypeExtensions
{
    public static string ToBloodString(this SubmissionType type)
        => type switch
        {
            SubmissionType.FirstBlood => "一血",
            SubmissionType.SecondBlood => "二血",
            SubmissionType.ThirdBlood => "三血",
            _ => throw new ArgumentException(type.ToString(), nameof(type))
        };
}

/// <summary>
/// 比赛事件类型
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EventType : byte
{
    /// <summary>
    /// 常规信息
    /// </summary>
    Normal = 0,

    /// <summary>
    /// 容器启动信息
    /// </summary>
    ContainerStart = 1,

    /// <summary>
    /// 容器销毁信息
    /// </summary>
    ContainerDestroy = 2,

    /// <summary>
    /// Flag 提交信息
    /// </summary>
    FlagSubmit = 3,

    /// <summary>
    /// 作弊信息
    /// </summary>
    CheatDetected = 4
}

/// <summary>
/// 提交类型
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubmissionType : byte
{
    /// <summary>
    /// 未解出
    /// </summary>
    Unaccepted = 0,

    /// <summary>
    /// 一血
    /// </summary>
    FirstBlood = 1,

    /// <summary>
    /// 二血
    /// </summary>
    SecondBlood = 2,

    /// <summary>
    /// 三血
    /// </summary>
    ThirdBlood = 3,

    /// <summary>
    /// 解出
    /// </summary>
    Normal = 4,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ParticipationStatus : byte
{
    /// <summary>
    /// 已报名
    /// </summary>
    Pending = 0,

    /// <summary>
    /// 已接受
    /// </summary>
    Accepted = 1,

    /// <summary>
    /// 已拒绝
    /// </summary>
    Denied = 2,

    /// <summary>
    /// 已取消
    /// </summary>
    Forfeited = 3,

    /// <summary>
    /// 未提交
    /// </summary>
    Unsubmitted = 4,
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChallengeType : byte
{
    /// <summary>
    /// 静态题目
    /// 所有队伍使用统一附件、统一 flag
    /// </summary>
    StaticAttachment = 0b00,

    /// <summary>
    /// 容器静态题目
    /// 所有队伍使用统一 docker，统一 flag
    /// </summary>
    StaticContainer = 0b01,

    /// <summary>
    /// 动态附件题目
    /// 随机分发附件，随附件实现 flag 特异性
    /// </summary>
    DynamicAttachment = 0b10,

    /// <summary>
    /// 容器动态题目
    /// 随机分发容器，动态 flag 随环境变量传入
    /// </summary>
    DynamicContainer = 0b11
}

public static class ChallengeTypeExtensions
{
    /// <summary>
    /// 是否为静态题目
    /// </summary>
    public static bool IsStatic(this ChallengeType type) => ((byte)type & 0b10) == 0;

    /// <summary>
    /// 是否为动态题目
    /// </summary>
    public static bool IsDynamic(this ChallengeType type) => ((byte)type & 0b10) != 0;

    /// <summary>
    /// 是否为附件题目
    /// </summary>
    public static bool IsAttachment(this ChallengeType type) => ((byte)type & 0b01) == 0;

    /// <summary>
    /// 是否为容器题目
    /// </summary>
    public static bool IsContainer(this ChallengeType type) => ((byte)type & 0b01) != 0;
}

/// <summary>
/// 题目标签
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChallengeTag : byte
{
    Misc = 0,
    Crypto = 1,
    Pwn = 2,
    Web = 3,
    Reverse = 4,
    Blockchain = 5,
    Forensics = 6,
    Hardware = 7,
    Mobile = 8,
    PPC = 9
}

/// <summary>
/// 判定结果
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AnswerResult : byte
{
    /// <summary>
    /// 成功提交
    /// </summary>
    FlagSubmitted = 0,

    /// <summary>
    /// 答案正确
    /// </summary>
    Accepted = 1,

    /// <summary>
    /// 答案错误
    /// </summary>
    WrongAnswer = 2,

    /// <summary>
    /// 提交的题目实例未找到
    /// </summary>
    NotFound = 3,

    /// <summary>
    /// 检测到抄袭
    /// </summary>
    CheatDetected = 4
}

public static class AnswerResultExtensions
{
    public static string ToShortString(this AnswerResult result)
        => result switch
        {
            AnswerResult.FlagSubmitted => "成功提交",
            AnswerResult.Accepted => "答案正确",
            AnswerResult.WrongAnswer => "答案错误",
            AnswerResult.NotFound => "实例未知",
            AnswerResult.CheatDetected => "作弊检测",
            _ => "??"
        };
}

/// <summary>
/// 缓存标识
/// </summary>
public static class CacheKey
{
    /// <summary>
    /// 积分榜缓存
    /// </summary>
    public static string ScoreBoard(int id) => $"_ScoreBoard_{id}";

    /// <summary>
    /// 比赛通知缓存
    /// </summary>
    public static string GameNotice(int id) => $"_GameNotice_{id}";

    /// <summary>
    /// 比赛基础信息缓存
    /// </summary>
    public const string BasicGameInfo = "_BasicGameInfo";

    /// <summary>
    /// 文章
    /// </summary>
    public const string Posts = "_Posts";
}