using System;
using System.Collections.Generic;

namespace TECHNOVA.Data;

public partial class Feedback
{
    public string FeedbackId { get; set; } = null!;

    public int TopicId { get; set; }

    public string Content { get; set; } = null!;

    public DateOnly FeedbackDate { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool RequiresResponse { get; set; }

    public string? Response { get; set; }

    public DateOnly? ResponseDate { get; set; }

    public virtual Topic Topic { get; set; } = null!;
}
