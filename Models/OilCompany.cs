using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

public class OilCompany
{
    public int id { get; set; }
    public string name { get; set; }
    public string industry { get; set; }

    public ITInfo ITInfo { get; set; }
}

