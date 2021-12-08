using AgendaFrontEnd.Data;
using AgendaFrontEnd.DTOs;
using AgendaFrontEnd.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AgendaFrontEnd.Pages.Contactos
{
    public class ComponentBaseContactos : ComponentBase
    {


        [Inject]
        public IAgendaService<dynamic> AgendaService { get; set; }



        [Inject] public IJSRuntime Js { get; set; }


        protected ContactoDTO contacto;

        protected dynamic response_rows;

        private bool _isEditing = false;


        public bool IsEditing()
        {
            return _isEditing;

        }



        public void CancelEditing()
        {
            _isEditing = false;
            ResetForm();
        }


        protected void ResetForm()
        {
            contacto = new();
            contacto.CorreoElectronicos = new();
            contacto.Telefonos = new();
            contacto.Telefonos.Add(new ContactoTelefonoDTO());
            contacto.CorreoElectronicos.Add(new());
        }
        protected override async Task OnInitializedAsync()
        {
            ResetForm();
            await LoadContactos();
        }


        protected void AddTelefono()
        {
            contacto.Telefonos.Add(new());
        }

        protected void RemoveTelefono()
        {
            if (contacto.Telefonos.Count > 1)
            {
                contacto.Telefonos.RemoveAt(contacto.Telefonos.Count - 1);
            }
        }

        protected void AddCorreo()
        {
            contacto.CorreoElectronicos.Add(new());
        }

        protected void RemoveCorreo()
        {
            if (contacto.CorreoElectronicos.Count > 1)
            {
                contacto.CorreoElectronicos.RemoveAt(contacto.CorreoElectronicos.Count - 1);
            }

        }


        protected async Task LoadContactos()
        {
            response_rows = await AgendaService.GetAsync("Contactos/GetListContactos");

        }


        protected async Task SaveContactos()
        {
            if (contacto.Id == 0)
            {
                try
                {
                    var response = await AgendaService.PostAsync("Contactos/CreateContacto", contacto);

                    if (response.status == 1)
                    {
                        await Js.InvokeVoidAsync("CustomSucess", $"Contacto {contacto.Nombre} {contacto.Apellido} creado correctamente");

                       await OnInitializedAsync();
                    }
                    else
                    {
                        await Js.InvokeVoidAsync("CustomError", $"{response.message}");

                    }

                }
                catch (System.Exception)
                {

                    await Js.InvokeVoidAsync("CustomError", "Ocurrio un error");
                }
            }
            else
            {
                await UpdateContacto();
            }

        }

        private async Task UpdateContacto()
        {

            try
            {
                var response = await AgendaService.PutAsync("Contactos/UpdateContacto/", contacto.Id, contacto);

                if (response.status == 1)
                {
                    await Js.InvokeVoidAsync("CustomSucess", $"Contacto {contacto.Nombre} {contacto.Apellido} actualizado correctamente");
                    await OnInitializedAsync();
                }
                else
                {
                    await Js.InvokeVoidAsync("CustomError", $"{response.message}");

                }

            }
            catch (System.Exception)
            {

                await Js.InvokeVoidAsync("CustomError", "Ocurrio un error");
            }

        }

        protected async Task DeleteContacto(dynamic contacto)
        {



            try
            {
                var response = await AgendaService.DeleteAsync("Contactos/DeleteContacto/", (int)contacto.id);

                if (response.status == 1)
                {
                    await Js.InvokeVoidAsync("CustomSucess", $"Contacto {contacto.nombre} {contacto.apellido} eliminado correctamente");
                    await OnInitializedAsync();

                }
                else
                {
                    await Js.InvokeVoidAsync("CustomError", $"{response.message}");

                }

            }
            catch (System.Exception)
            {

                await Js.InvokeVoidAsync("CustomError", "Ocurrio un error");
            }
            await OnInitializedAsync();
        }

        protected async Task LoadContacto(int id)
        {
            try
            {
                var response = await AgendaService.GetAsync($"Contactos/GetContacto/{id}");
                contacto = JsonConvert.DeserializeObject<ContactoDTO>(response.contacto.ToString());
                _isEditing = true;

            }
            catch (System.Exception)
            {

                await Js.InvokeVoidAsync("CustomError", "No se pudo solicitar el contacto"); ;
            }


        }

    }
}
