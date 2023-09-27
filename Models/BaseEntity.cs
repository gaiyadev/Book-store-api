﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookstoreAPI.Models;

public abstract class BaseEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
    
    [Column("updated_at")]
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
}