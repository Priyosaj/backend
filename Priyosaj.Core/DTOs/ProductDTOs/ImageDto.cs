using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Priyosaj.Core.Entities;
using Priyosaj.Core.MapperProfile;

namespace Priyosaj.Core.DTOs.ProductDTOs;
public class ImageDto : IMapFrom<FileEntity>
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
}