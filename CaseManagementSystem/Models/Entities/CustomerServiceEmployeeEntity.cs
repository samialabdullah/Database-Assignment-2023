﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CaseManagementSystem.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
internal class CustomerServiceEmployeeEntity
{
    [Key]
    public int Id { get; set; }
    [StringLength(50)]
    public string FirstName { get; set; } = null!;
    [StringLength(50)]
    public string LastName { get; set; } = null!;
    [StringLength(100)]
    public string Email { get; set; } = null!;
    [Column(TypeName = "char(13)")]
    public string PhoneNumber { get; set; } = null!;
    public ICollection<CommentEntity> Situations { get; set; } = new HashSet<CommentEntity>();
}