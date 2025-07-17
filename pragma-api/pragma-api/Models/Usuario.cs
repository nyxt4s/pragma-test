using System;
using System.Collections.Generic;

namespace pragma_api.Models;

/// <summary>
/// Representa un usuario del sistema.
/// </summary>
public partial class Usuario
{
    /// <summary>
    /// Identificador único del usuario.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo del usuario.
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// RUT (Rol Único Tributario) del usuario. Es único por usuario.
    /// </summary>
    public string Rut { get; set; } = null!;

    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    public string? Correo { get; set; }

    /// <summary>
    /// Fecha de nacimiento del usuario.
    /// </summary>
    public DateOnly FechaNacimiento { get; set; }
}