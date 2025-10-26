using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Login.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Login.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedDataAsync(AppDbContext context)
    {

        await context.Database.EnsureCreatedAsync();


        if (await context.Usuarios.AnyAsync())
        {
            return;
        }

        var rolEntrenador = await context.Roles.FirstOrDefaultAsync(r => r.nombre == "Entrenador");
        var rolAlumno = await context.Roles.FirstOrDefaultAsync(r => r.nombre == "Alumno");

        if (rolEntrenador == null || rolAlumno == null)
        {
    
            return;
        }

        var entrenadorPrueba = new Usuario
        {
            Id = Guid.NewGuid(),
            email = "entrenador@test.com",
            password = "password123",
            nombre = "Carlos",
            apellido = "Maestro",
            celular = "1122334455",
            activo = true,
            rol_id = rolEntrenador.Id,
            creado_en = DateTimeOffset.UtcNow,
            actualizado_en = DateTimeOffset.UtcNow,
            AlumnosEntrenados = new List<Alumno>() 
        };

        await context.Usuarios.AddAsync(entrenadorPrueba);

 
        var alumnoPrueba = new Usuario
        {
            Id = Guid.NewGuid(),
            email = "alumno@test.com",
            password = "password123", 
            nombre = "Juan",
            apellido = "Perez",
            activo = true,
            celular = "5544332211",
            rol_id = rolAlumno.Id,
            creado_en = DateTimeOffset.UtcNow,
            actualizado_en = DateTimeOffset.UtcNow
        };

        await context.Usuarios.AddAsync(alumnoPrueba);
        var infoAlumno = new Alumno
        {
            Id = Guid.NewGuid(),
            altura_cm = 180,
            peso_kg = 75,
            fecha_nacimiento = new DateTimeOffset(new DateTime(2000, 5, 15), TimeSpan.Zero),
            Direccion = "Calle Falsa 123",
            notas = "Alumno de prueba inicial",

            UsuarioId = alumnoPrueba.Id,   
            entrenador_id = entrenadorPrueba.Id, 

            Usuario = alumnoPrueba, 

            Entrenador = entrenadorPrueba
        };

        await context.Alumnos.AddAsync(infoAlumno);

        await context.SaveChangesAsync();
    }
}
