namespace vetsoft_c.DTOs
{
  public class DashboardStatsDTO
  {
      public long TotalClientes { get; set; }
      public long TotalPacientes { get; set; }
      public long TotalVeterinarios { get; set; }
      public long TotalServicios { get; set; }
      public long TotalCitas { get; set; }
      public long TotalUsuarios { get; set; }
      public long CitasHoy { get; set; }
      public long CitasPendientes { get; set; }
  }
}
