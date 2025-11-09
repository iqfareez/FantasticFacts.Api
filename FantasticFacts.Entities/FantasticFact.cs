using System.ComponentModel.DataAnnotations;

namespace FantasticFacts.Entities;

/// <summary>
/// The Fantastic Fact entity.
/// </summary>
public class FantasticFact
{
    /// <summary>
    /// The ID of the fantastic fact.
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// The content of the fantastic fact.
    /// </summary>
    public string Content { get; set; }
}