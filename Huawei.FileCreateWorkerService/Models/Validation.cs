using System;
using System.Collections.Generic;

namespace Huawei.RabbitMqSubscriberService.Models;

public partial class Validation
{
    public int recID { get; set; }

    public string userID { get; set; }

    public string? mailAddress { get; set; }

    public DateTime? RequestDate { get; set; }

    public DateTime? ResultDate { get; set; }

    public string? Result { get; set; }

    public string? ResultDescription { get; set; }

    public Guid BatchId { get; set; }
}
