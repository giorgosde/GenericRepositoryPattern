﻿using GenericRepository.Dal.Entities;

namespace GenericRepository.Dal;

public interface IVehicleRepository : IGenericRepository<Vehicle>
{
}
public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
{
    protected readonly DatabaseContext _context;

    public VehicleRepository(DatabaseContext context) : base(context)
        => _context = context;
}
