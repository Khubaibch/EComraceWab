using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EComraceWab.Models;

namespace EComraceWab.Entites;

public class Category : AuditedEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }


}
