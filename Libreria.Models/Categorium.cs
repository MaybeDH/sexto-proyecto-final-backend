﻿namespace Libreria.Models;

public partial class Categorium
{
    public int Id { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
