using CapaEntidad;
using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas4Capas
{
    public partial class MenuPrincipal : Form
    {
        private EmpleadoLogica empleadoLogica;
        private Empleado empleadoSeleccionado;
        private CategoriaLogica categoriaLogica;
        private Categoria categoriaSeleccionada;
        private ProductoLogica productoLogica;
        private Producto productoSeleccionado;
        private ClienteLogica clienteLogica;
        private Cliente clienteSeleccionado;
        private VentaLogica ventaLogica;
        private Venta ventaSeleccionada;

        public MenuPrincipal()
        {
            InitializeComponent();
            empleadoLogica = new EmpleadoLogica();
            categoriaLogica = new CategoriaLogica();
            productoLogica = new ProductoLogica();
            clienteLogica = new ClienteLogica();
            ventaLogica = new VentaLogica();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            panelEmpleados.Visible = false;
            panelCategorias.Visible = false;
            panelProductos.Visible = false;
            panelClientes.Visible = false;
            panelVentas.Visible = false;

            // Cargar datos para comboCliente
            List<Cliente> clientes = clienteLogica.ObtenerClientes();
            comboCliente.DataSource = clientes;
            comboCliente.DisplayMember = "Nombres"; // Ajusta esto según la propiedad que represente el nombre del cliente
            comboCliente.ValueMember = "IdCliente"; // Ajusta esto según la propiedad que represente el Id del cliente

            // Cargar datos para comboProducto
            List<Producto> productos = productoLogica.ObtenerProductos();
            comboProducto.DataSource = productos;
            comboProducto.DisplayMember = "Nombre"; // Ajusta esto según la propiedad que represente el nombre del producto
            comboProducto.ValueMember = "IdProducto"; // Ajusta esto según la propiedad que represente el Id del producto

            CargarTiposEmpleados();
            CargarEmpleados();
            CargarCategorias();
            CargarProductos();
            CargarComboCategorias();
            CargarClientes();
            CargarVentas();

            InicializarEventos();// Configurar eventos
        }

        private void CargarVentas()
        {
            listadoVentas.DataSource = ventaLogica.ObtenerVentas();
        }

        private void CargarClientes()
        {
            // Obtener la lista de clientes desde la lógica de negocios
            List<Cliente> clientes = clienteLogica.ObtenerClientes();

            // Mostrar la lista en el DataGridView
            listadoClientes.DataSource = clientes;
        }

        private void CargarProductos()
        {
            // Obtener la lista de productos desde la lógica de negocios
            List<Producto> productos = productoLogica.ObtenerProductos();

            // Mostrar la lista en el DataGridView
            listadoProductos.DataSource = productos;
        }

        private void CargarCategorias()
        {
            listadoCategoria.DataSource = categoriaLogica.ObtenerCategorias();
        }

        private void CargarTiposEmpleados()
        {
            // Puedes modificar esta lista según los tipos de empleados que necesites
            List<string> tipos = new List<string> { "Administrador", "Vendedor" };
            comboTipo.DataSource = tipos;
        }
        private void CargarEmpleados()
        {
            listaEmpleados.DataSource = empleadoLogica.ObtenerEmpleados();
        }

        private void CargarComboCategorias()
        {
            // Obtener la lista de categorías desde la lógica de negocios
            List<Categoria> categorias = categoriaLogica.ObtenerCategorias();

            // Mostrar la lista en el ComboBox
            comboCategoria.DataSource = categorias;
            comboCategoria.DisplayMember = "Nombre"; // Ajusta esto según la propiedad que represente el nombre de la categoría
            comboCategoria.ValueMember = "IdCategoria"; // Ajusta esto según la propiedad que represente el ID de la categoría
        }

        private void LimpiarCamposEmpleado()
        {
            txtNombres.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtDni.Text = string.Empty;
            comboTipo.SelectedIndex = -1;
            txtSueldo.Text = string.Empty;
            txtDireccion.Text = string.Empty;

            // Limpiar también la variable de empleado seleccionado
            empleadoSeleccionado = null;
        }

        private void LimpiarCamposCategoria()
        {
            txtNombreCategoria.Text = string.Empty;

            // Limpiar también la variable de categoría seleccionada
            categoriaSeleccionada = null;
        }

        private void LimpiarCamposProducto()
        {
            // Limpiar los campos del formulario
            txtNombreProducto.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            comboCategoria.SelectedIndex = -1;

            // Limpiar la variable de producto seleccionado
            productoSeleccionado = null;
        }

        private void LimpiarCamposCliente()
        {
            // Limpiar los campos del formulario
            txtNombresCliente.Text = string.Empty;
            txtApellidosCliente.Text = string.Empty;
            txtDniCliente.Text = string.Empty;
            txtCorreoCliente.Text = string.Empty;
            txtDireccionCliente.Text = string.Empty;

            // Limpiar la variable de cliente seleccionado
            clienteSeleccionado = null;
        }

        private void LimpiarCamposVenta()
        {
            txtCantidad.Text = string.Empty;
            fechaVentaPicker.Value = DateTime.Now; // Así se limpia el DateTimePicker
            txtPrecioTotalVenta.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            comboCliente.SelectedIndex = -1; // Así se limpia el ComboBox
            comboProducto.SelectedIndex = -1; // Así se limpia el ComboBox
            // Limpiar la variable de venta seleccionada
            ventaSeleccionada = null;
        }

        private void listaEmpleados_SelectionChanged(object sender, EventArgs e)
        {
            // Manejar el evento de cambio de selección en el DataGridView
            if (listaEmpleados.SelectedRows.Count > 0)
            {
                // Obtener el empleado seleccionado de la fila actual
                empleadoSeleccionado = (Empleado)listaEmpleados.SelectedRows[0].DataBoundItem;

                // Mostrar los datos del empleado en los TextBox y ComboBox
                txtNombres.Text = empleadoSeleccionado.Nombres;
                txtApellidos.Text = empleadoSeleccionado.Apellidos;
                txtDni.Text = empleadoSeleccionado.Dni;
                comboTipo.Text = empleadoSeleccionado.Tipo;
                txtSueldo.Text = empleadoSeleccionado.Sueldo.ToString();
                txtDireccion.Text = empleadoSeleccionado.Direccion;
            }
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            panelEmpleados.Visible = true;
            panelCategorias.Visible = false;
            panelProductos.Visible = false;
            panelClientes.Visible = false;
            panelVentas.Visible = false;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            panelEmpleados.Visible = false;
            panelCategorias.Visible = false;
            panelProductos.Visible = false;
            panelClientes.Visible = false;
            panelVentas.Visible = false;
        }

        private void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            LimpiarCamposCategoria();
        }

        private void btnNuevoEmpleado_Click(object sender, EventArgs e)
        {
            LimpiarCamposEmpleado();
        }

        private void btnGuardarEmpleado_Click(object sender, EventArgs e)
        {
            // Validaciones de datos antes de guardar (puedes personalizar según tus necesidades)
            if (string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidos.Text) || string.IsNullOrEmpty(txtDni.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear un nuevo objeto Empleado con los datos del formulario
            Empleado nuevoEmpleado = new Empleado
            {
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                Dni = txtDni.Text,
                Tipo = comboTipo.Text,
                Sueldo = Convert.ToInt32(txtSueldo.Text), // Asegúrate de manejar conversiones seguras
                Direccion = txtDireccion.Text
            };

            // Llamar al método de la lógica para insertar el nuevo empleado
            empleadoLogica.InsertarEmpleado(nuevoEmpleado);

            // Actualizar la vista con los empleados actualizados
            CargarEmpleados();

            // Limpiar los campos después de guardar
            LimpiarCamposEmpleado();
        }

        private void btnEditarEmpleado_Click(object sender, EventArgs e)
        {
            if (empleadoSeleccionado != null)
            {
                // Actualizar el objeto empleadoSeleccionado con los datos modificados
                empleadoSeleccionado.Nombres = txtNombres.Text;
                empleadoSeleccionado.Apellidos = txtApellidos.Text;
                empleadoSeleccionado.Dni = txtDni.Text;
                empleadoSeleccionado.Tipo = comboTipo.Text;
                empleadoSeleccionado.Sueldo = Convert.ToInt32(txtSueldo.Text);
                empleadoSeleccionado.Direccion = txtDireccion.Text;

                // Llamar al método de la lógica para editar el empleado
                empleadoLogica.EditarEmpleado(empleadoSeleccionado);

                // Actualizar la vista con los empleados actualizados
                CargarEmpleados();

                // Limpiar los campos después de guardar
                LimpiarCamposEmpleado();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un empleado para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarEmpleado_Click(object sender, EventArgs e)
        {
            if (empleadoSeleccionado != null)
            {
                // Mostrar un cuadro de diálogo de confirmación
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este empleado?", "Confirmación de Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Llamar al método de la lógica para eliminar el empleado
                    empleadoLogica.EliminarEmpleado(empleadoSeleccionado.IdEmpleado);

                    // Actualizar la vista con los empleados actualizados
                    CargarEmpleados();

                    // Limpiar los campos después de eliminar
                    LimpiarCamposEmpleado();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un empleado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            // Validaciones de datos antes de guardar (puedes personalizar según tus necesidades)
            if (string.IsNullOrEmpty(txtNombreCategoria.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear un nuevo objeto Categoria con los datos del formulario
            Categoria nuevaCategoria = new Categoria
            {
                Nombre = txtNombreCategoria.Text
            };

            // Llamar al método de la lógica para insertar la nueva categoría
            categoriaLogica.InsertarCategoria(nuevaCategoria);

            // Actualizar la vista con las categorías actualizadas
            CargarCategorias();

            CargarComboCategorias(); // Actualizamos también el combo de categorías

            // Limpiar los campos después de guardar
            LimpiarCamposCategoria();
        }

        private void btnEditarCategoria_Click(object sender, EventArgs e)
        {
            if (categoriaSeleccionada != null)
            {
                // Actualizar el objeto categoriaSeleccionada con los datos modificados
                categoriaSeleccionada.Nombre = txtNombreCategoria.Text;

                // Llamar al método de la lógica para editar la categoría
                categoriaLogica.EditarCategoria(categoriaSeleccionada);

                // Actualizar la vista con las categorías actualizadas
                CargarCategorias();

                CargarComboCategorias();

                // Limpiar los campos después de guardar
                LimpiarCamposCategoria();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una categoría para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
            if (categoriaSeleccionada != null)
            {
                // Mostrar un cuadro de diálogo de confirmación
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta categoría?", "Confirmación de Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Llamar al método de la lógica para eliminar la categoría
                    categoriaLogica.EliminarCategoria(categoriaSeleccionada.IdCategoria);

                    // Actualizar la vista con las categorías actualizadas
                    CargarCategorias();

                    CargarComboCategorias();

                    // Limpiar los campos después de eliminar
                    LimpiarCamposCategoria();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una categoría para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            panelCategorias.Visible = true;
            panelEmpleados.Visible = false;
            panelProductos.Visible = false;
            panelClientes.Visible = false;
            panelVentas.Visible = false;
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = true;
            panelCategorias.Visible = false;
            panelEmpleados.Visible = false;
            panelClientes.Visible = false;
            panelVentas.Visible = false;
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            LimpiarCamposProducto();
        }

        private void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            // Validaciones de datos antes de guardar (puedes personalizar según tus necesidades)
            if (string.IsNullOrEmpty(txtNombreProducto.Text) || string.IsNullOrEmpty(txtPrecio.Text) || string.IsNullOrEmpty(txtStock.Text) || comboCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear un nuevo objeto Producto con los datos del formulario
            Producto nuevoProducto = new Producto
            {
                Nombre = txtNombreProducto.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Stock = Convert.ToInt32(txtStock.Text),
                IdCategoria = Convert.ToInt32(comboCategoria.SelectedValue)
            };

            // Llamar al método de la lógica para insertar el nuevo producto
            productoLogica.InsertarProducto(nuevoProducto);

            // Actualizar la vista con los productos actualizados
            CargarProductos();

            // Limpiar los campos después de guardar
            LimpiarCamposProducto();
        }

        private void btnEditarProducto_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado != null)
            {
                // Actualizar el objeto productoSeleccionado con los datos modificados
                productoSeleccionado.Nombre = txtNombreProducto.Text;
                productoSeleccionado.Precio = Convert.ToDecimal(txtPrecio.Text);
                productoSeleccionado.Stock = Convert.ToInt32(txtStock.Text);
                productoSeleccionado.IdCategoria = Convert.ToInt32(comboCategoria.SelectedValue);

                // Llamar al método de la lógica para editar el producto
                productoLogica.EditarProducto(productoSeleccionado);

                // Actualizar la vista con los productos actualizados
                CargarProductos();

                // Limpiar los campos después de editar
                LimpiarCamposProducto();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado != null)
            {
                // Mostrar un cuadro de diálogo de confirmación
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmación de Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Llamar al método de la lógica para eliminar el producto
                    productoLogica.EliminarProducto(productoSeleccionado.IdProducto);

                    // Actualizar la vista con los productos actualizados
                    CargarProductos();

                    // Limpiar los campos después de eliminar
                    LimpiarCamposProducto();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listadoProductos_SelectionChanged(object sender, EventArgs e)
        {
            // Manejar el evento de cambio de selección en el DataGridView
            if (listadoProductos.SelectedRows.Count > 0)
            {
                // Obtener el producto seleccionado de la fila actual
                productoSeleccionado = (Producto)listadoProductos.SelectedRows[0].DataBoundItem;

                // Mostrar los datos del producto en los TextBoxes y ComboBox
                txtNombreProducto.Text = productoSeleccionado.Nombre;
                txtPrecio.Text = productoSeleccionado.Precio.ToString();
                txtStock.Text = productoSeleccionado.Stock.ToString();
                comboCategoria.SelectedValue = productoSeleccionado.IdCategoria;
            }
        }

        private void listadoCategoria_SelectionChanged(object sender, EventArgs e)
        {
            if (listadoCategoria.SelectedRows.Count > 0)
            {
                // Obtener la categoría seleccionada de la fila actual
                categoriaSeleccionada = (Categoria)listadoCategoria.SelectedRows[0].DataBoundItem;

                // Mostrar los datos de la categoría en los TextBox
                txtNombreCategoria.Text = categoriaSeleccionada.Nombre;
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            LimpiarCamposCliente();
        }

        private void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            // Validaciones de datos antes de guardar (puedes personalizar según tus necesidades)
            if (string.IsNullOrEmpty(txtNombresCliente.Text) || string.IsNullOrEmpty(txtApellidosCliente.Text) || string.IsNullOrEmpty(txtDniCliente.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear un nuevo objeto Cliente con los datos del formulario
            Cliente nuevoCliente = new Cliente
            {
                Nombres = txtNombresCliente.Text,
                Apellidos = txtApellidosCliente.Text,
                Dni = txtDniCliente.Text,
                Correo = txtCorreoCliente.Text,
                Direccion = txtDireccionCliente.Text
            };

            // Llamar al método de la lógica para insertar el nuevo cliente
            clienteLogica.InsertarCliente(nuevoCliente);

            // Actualizar la vista con los clientes actualizados
            CargarClientes();

            // Limpiar los campos después de guardar
            LimpiarCamposCliente();
        }

        private void btnEditarCliente_Click(object sender, EventArgs e)
        {
            if (clienteSeleccionado != null)
            {
                // Actualizar el objeto clienteSeleccionado con los datos modificados
                clienteSeleccionado.Nombres = txtNombresCliente.Text;
                clienteSeleccionado.Apellidos = txtApellidosCliente.Text;
                clienteSeleccionado.Dni = txtDniCliente.Text;
                clienteSeleccionado.Correo = txtCorreoCliente.Text;
                clienteSeleccionado.Direccion = txtDireccionCliente.Text;

                // Llamar al método de la lógica para editar el cliente
                clienteLogica.EditarCliente(clienteSeleccionado);

                // Actualizar la vista con los clientes actualizados
                CargarClientes();

                // Limpiar los campos después de editar
                LimpiarCamposCliente();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un cliente para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            if (clienteSeleccionado != null)
            {
                // Mostrar un cuadro de diálogo de confirmación
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este cliente?", "Confirmación de Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Llamar al método de la lógica para eliminar el cliente
                    clienteLogica.EliminarCliente(clienteSeleccionado.IdCliente);

                    // Actualizar la vista con los clientes actualizados
                    CargarClientes();

                    // Limpiar los campos después de eliminar
                    LimpiarCamposCliente();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un cliente para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listadoClientes_SelectionChanged(object sender, EventArgs e)
        {
            // Manejar el evento de cambio de selección en el DataGridView
            if (listadoClientes.SelectedRows.Count > 0)
            {
                // Obtener el cliente seleccionado de la fila actual
                clienteSeleccionado = (Cliente)listadoClientes.SelectedRows[0].DataBoundItem;

                // Mostrar los datos del cliente en los TextBox
                txtNombresCliente.Text = clienteSeleccionado.Nombres;
                txtApellidosCliente.Text = clienteSeleccionado.Apellidos;
                txtDniCliente.Text = clienteSeleccionado.Dni;
                txtCorreoCliente.Text = clienteSeleccionado.Correo;
                txtDireccionCliente.Text = clienteSeleccionado.Direccion;
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            panelClientes.Visible = true;
            /*panelProductos.Visible = false;
            panelCategorias.Visible = false;
            panelEmpleados.Visible = false;*/
        }

        private decimal ObtenerPrecioProducto(int idProducto)
        {
            List<Producto> productos = productoLogica.ObtenerProductos();

            // Buscar el producto por ID en la lista
            Producto producto = productos.FirstOrDefault(p => p.IdProducto == idProducto);

            // Si encontramos el producto, devolvemos su precio
            if (producto != null)
            {
                return producto.Precio;
            }

            // Si no encontramos el producto, devolvemos un valor predeterminado o lanzamos una excepción según tu lógica
            throw new InvalidOperationException("No se pudo encontrar el precio del producto.");
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            // Validaciones de datos antes de guardar (puedes personalizar según tus necesidades)
            if (string.IsNullOrEmpty(txtCantidad.Text) || fechaVentaPicker.Value == null ||
                string.IsNullOrEmpty(txtPrecioTotalVenta.Text) || comboCliente.SelectedIndex == -1 ||
                comboProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener el producto seleccionado
            Producto productoSeleccionado = (Producto)comboProducto.SelectedItem;

            // Calcular el precio total
            decimal precioTotal = productoSeleccionado.Precio * Convert.ToInt32(txtCantidad.Text);

            // Crear un nuevo objeto Venta con los datos del formulario
            Venta nuevaVenta = new Venta
            {
                Cantidad = Convert.ToInt32(txtCantidad.Text),
                FechaVenta = fechaVentaPicker.Value,
                PrecioTotal = precioTotal,
                IdCliente = Convert.ToInt32(comboCliente.SelectedValue),
                IdProducto = productoSeleccionado.IdProducto
            };

            // Llamar al método de la lógica para insertar la nueva venta
            ventaLogica.InsertarVenta(nuevaVenta);

            // Actualizar la vista con las ventas actualizadas
            CargarVentas();

            // Limpiar los campos después de guardar
            LimpiarCamposVenta();
        }


        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            LimpiarCamposVenta();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            panelVentas.Visible = true;
            panelCategorias.Visible = true;
            panelEmpleados.Visible = false;
            panelProductos.Visible = false;
            panelClientes.Visible = false;
        }

        private void listadoVentas_SelectionChanged(object sender, EventArgs e)
        {
            // Manejar el evento de cambio de selección en el DataGridView de ventas
            if (listadoVentas.SelectedRows.Count > 0)
            {
                // Obtener la venta seleccionada de la fila actual
                Venta ventaSeleccionada = (Venta)listadoVentas.SelectedRows[0].DataBoundItem;

                // Mostrar los datos de la venta en los TextBoxes, DateTimePicker y ComboBox
                txtCantidad.Text = ventaSeleccionada.Cantidad.ToString();
                fechaVentaPicker.Value = ventaSeleccionada.FechaVenta;
                txtPrecioTotalVenta.Text = ventaSeleccionada.PrecioTotal.ToString();
                comboCliente.SelectedValue = ventaSeleccionada.IdCliente;
                comboProducto.SelectedValue = ventaSeleccionada.IdProducto;
            }
        }

        private void comboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboProducto.SelectedItem != null)
            {
                // Obtener el producto seleccionado
                Producto productoSeleccionado = (Producto)comboProducto.SelectedItem;

                // Mostrar el precio del producto en txtPrecioVenta
                txtPrecioVenta.Text = productoSeleccionado.Precio.ToString();

                // Actualizar el precio total si la cantidad también está especificada
                ActualizarPrecioTotal();
            }

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            // Actualizar el precio total cuando cambia la cantidad
            ActualizarPrecioTotal();
        }

        private void ActualizarPrecioTotal()
        {
            if (comboProducto.SelectedIndex != -1 && !string.IsNullOrEmpty(txtCantidad.Text))
            {
                // Obtener el producto seleccionado
                Producto productoSeleccionado = (Producto)comboProducto.SelectedItem;

                // Calcular y mostrar el precio total en txtPrecioTotalVenta
                decimal precioTotal = productoSeleccionado.Precio * Convert.ToInt32(txtCantidad.Text);
                txtPrecioTotalVenta.Text = precioTotal.ToString();
            }
        }

        private void InicializarEventos()
        {
            comboProducto.SelectedIndexChanged += comboProducto_SelectedIndexChanged;
            txtCantidad.TextChanged += txtCantidad_TextChanged;
        }
    }
}
