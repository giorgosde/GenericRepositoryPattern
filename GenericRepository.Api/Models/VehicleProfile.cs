using AutoMapper;
using GenericRepository.Dal.Entities;

namespace GenericRepository.Api.Models;

public class VehicleProfile: Profile
{
    public VehicleProfile()
    {
        CreateMap<VehicleDto, Vehicle>();
        CreateMap<Vehicle, VehicleDto>();
    }
}
