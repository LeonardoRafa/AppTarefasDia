using AppTarefasDia.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTarefasDia.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Formulario : ContentPage
    {
        public Formulario()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Tarefa tarefa_selecionada = new Tarefa();

                if (BindingContext != null)
                    tarefa_selecionada = BindingContext as Tarefa;

                Tarefa t = new Tarefa
                {
                    Id = tarefa_selecionada.Id,
                    Titulo = txt_titulo.Text,
                    Descricao = txt_descricao.Text,
                    Status = false,
                    Data_Vencimento = dtpck_data_vencimento.Date,
                };

                if (t.Id == 0)
                {
                    await App.Database.Insert(t);
                    await DisplayAlert("Deu certo!", "Tarefa Inserida", "OK");
                }
                else
                {
                    await App.Database.Update(t);
                    await DisplayAlert("Deu certo!", "Tarefa Atualizada", "OK");
                }

                App.Current.MainPage = new NavigationPage(new Listagem());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}