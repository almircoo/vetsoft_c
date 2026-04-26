using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetsoft_c.Data;

namespace vetsoft_c.Services
{
    public class CodigoService
    {
        private readonly AppDbContext _context;
        private const int CODIGO_LENGTH = 6;

        public CodigoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerarCodigoClienteAsync()
        {
            return await GenerarCodigoUnicoAsync("CL");
        }

        public async Task<string> GenerarCodigoPacienteAsync()
        {
            return await GenerarCodigoUnicoAsync("PA");
        }

        public async Task<string> GenerarCodigoVeterinarioAsync()
        {
            return await GenerarCodigoUnicoAsync("VE");
        }

        public async Task<string> GenerarCodigoServicioAsync()
        {
            return await GenerarCodigoUnicoAsync("SE");
        }

        public async Task<string> GenerarCodigoCitaAsync()
        {
            return await GenerarCodigoUnicoAsync("CI");
        }

        public async Task<string> GenerarCodigoUsuarioAsync()
        {
            return await GenerarCodigoUnicoAsync("US");
        }

        private async Task<string> GenerarCodigoUnicoAsync(string prefijo)
        {
            string codigo;
            int numero = 1;
            bool existe = true;

            while (existe)
            {
                codigo = $"{prefijo}-{numero:D4}";

                // Verificar en todas las tablas
                existe = await CodigoExisteAsync(codigo);

                if (!existe)
                {
                    return codigo;
                }

                numero++;
            }

            throw new InvalidOperationException($"No se pudo generar un código único con prefijo {prefijo}");
        }

        private async Task<bool> CodigoExisteAsync(string codigo)
        {
            var enClientes = await _context.Clientes.AnyAsync(c => c.Codigo == codigo);
            if (enClientes) return true;

            var enPacientes = await _context.Pacientes.AnyAsync(p => p.Codigo == codigo);
            if (enPacientes) return true;

            var enVeterinarios = await _context.Veterinarios.AnyAsync(v => v.Codigo == codigo);
            if (enVeterinarios) return true;

            var enServicios = await _context.Servicios.AnyAsync(s => s.Codigo == codigo);
            if (enServicios) return true;

            var enCitas = await _context.Citas.AnyAsync(c => c.Codigo == codigo);
            if (enCitas) return true;

            var enUsuarios = await _context.Usuarios.AnyAsync(u => u.Codigo == codigo);
            if (enUsuarios) return true;

            return false;
        }
    }
}
