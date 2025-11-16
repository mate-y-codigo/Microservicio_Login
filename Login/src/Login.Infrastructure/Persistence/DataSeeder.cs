using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Login.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net; 

namespace Login.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            if (await context.Usuarios.AnyAsync())
            {
                return; 
            }

            // --- 1. BUSCAR LOS TRES ROLES ---
            var rolAdmin = await context.Roles.FirstOrDefaultAsync(r => r.nombre == "Admin");
            var rolEntrenador = await context.Roles.FirstOrDefaultAsync(r => r.nombre == "Entrenador");
            var rolAlumno = await context.Roles.FirstOrDefaultAsync(r => r.nombre == "Alumno");

            // Si los roles no se insertaron con HasData, el seeder no puede continuar.
            if (rolAdmin == null || rolEntrenador == null || rolAlumno == null)
            {
                // Opcional: Lanza una excepción o loguea un error si los roles no existen
                return;
            }

            // --- 2. CREAR UN "SUPER ADMIN" ---
            // Este es el usuario raíz para iniciar el sistema
            var superAdmin = new Usuario
            {
                Id = Guid.NewGuid(),
                email = "admin@fitcode.com",
                password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                nombre = "Admin",
                apellido = "FitCode",
                celular = "0000000000",
                activo = true,
                rol_id = rolAdmin.Id, 
                creado_en = DateTimeOffset.UtcNow,
                actualizado_en = DateTimeOffset.UtcNow,
                AlumnosEntrenados = new List<Alumno>()
            };
            await context.Usuarios.AddAsync(superAdmin);


            // --- 3. CREAR UN "ENTRENADOR" DE PRUEBA ---
            var entrenadorPrueba = new Usuario
            {
                Id = Guid.NewGuid(),
                email = "entrenador@test.com",
                password = BCrypt.Net.BCrypt.HashPassword("password123"),
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


            // --- 4. CREAR UN "ALUMNO" DE PRUEBA ---
            var alumnoPrueba = new Usuario
            {
                Id = Guid.NewGuid(),
                email = "alumno@test.com",
                password = BCrypt.Net.BCrypt.HashPassword("password123"),
                nombre = "Juan",
                apellido = "Perez",
                activo = true,
                celular = "5544332211",
                altura = 180,
                peso = 75,
                rol_id = rolAlumno.Id, 
                creado_en = DateTimeOffset.UtcNow,
                actualizado_en = DateTimeOffset.UtcNow
                
            };
            await context.Usuarios.AddAsync(alumnoPrueba);

            // --- 5. CREAR LA INFO DEL ALUMNO ---
            var infoAlumno = new Alumno
            {
                Id = Guid.NewGuid(),
                fecha_nacimiento = new DateTimeOffset(new DateTime(2000, 5, 15), TimeSpan.Zero),
                Direccion = "Calle Falsa 123",
                notas = "Alumno de prueba inicial",
                UsuarioId = alumnoPrueba.Id,   // Link 1-a-1 al usuario "alumnoPrueba"
                entrenador_id = entrenadorPrueba.Id, // Link al "entrenadorPrueba"
                Usuario = alumnoPrueba,
                Entrenador = entrenadorPrueba
            };
            await context.Alumnos.AddAsync(infoAlumno);

            // --- 6. GUARDAR TODO EN LA BASE DE DATOS ---
            await context.SaveChangesAsync();
        }
    }
}
