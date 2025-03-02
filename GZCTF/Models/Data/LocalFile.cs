﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace CTFServer.Models;

[Index(nameof(Hash))]
public class LocalFile
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// 文件哈希
    /// </summary>
    [MaxLength(64)]
    public string Hash { get; set; } = string.Empty;

    /// <summary>
    /// 文件名
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 引用计数
    /// </summary>
    [JsonIgnore]
    public uint ReferenceCount { get; set; } = 1;

    /// <summary>
    /// 文件存储位置
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public string Location => $"{Hash[..2]}/{Hash[2..4]}";

    /// <summary>
    /// 获取文件Url
    /// </summary>
    public string Url(string? filename = null) => $"/assets/{Hash}/{filename ?? Name}";
}