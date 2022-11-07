using API.negocios;
using API.Model;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region CLIENTE
app.MapPost("/cliente/Listar", (dbCliente cliente) =>
{
    Cliente clienteForm = new Cliente();
    return clienteForm.Listar(cliente.id, cliente.nome, cliente.cpf, cliente.sexo, cliente.tipo, cliente.situacao);
}).WithTags("Cliente");

app.MapGet("/cliente/Listar", () =>
{
    Cliente clienteForm = new Cliente();
    return clienteForm.Listar();
}).WithTags("Cliente");

app.MapDelete("/cliente/{id}", (string id) =>
{
    Cliente clienteForm = new Cliente();
    return clienteForm.Excluir(id);
}).WithTags("Cliente");

app.MapPut("/cliente/{id}", (string id, dbCliente cliente) =>
{
    Cliente clienteForm = new Cliente();
    return clienteForm.Alterar(id, cliente.nome, cliente.cpf, cliente.sexo, cliente.tipo, cliente.situacao);
}).WithTags("Cliente");

app.MapPost("/cliente", (dbCliente cliente) =>
{
    Cliente clienteForm = new Cliente();
    return clienteForm.Inserir(cliente.nome, cliente.cpf, cliente.sexo, cliente.tipo, cliente.situacao);
}).WithTags("Cliente");
#endregion

#region TIPO CLIENTE
app.MapGet("/tipo/listar", () =>
{
    TipoCliente tipoCliente = new TipoCliente();
    return tipoCliente.Listar();
}).WithTags("Tipo Cliente");

app.MapDelete("/tipo/{id}", (string id) =>
{
    TipoCliente tipoCliente = new TipoCliente();
    return tipoCliente.Excluir(id);
}).WithTags("Tipo Cliente");

app.MapPut("/tipo/{id}", (string id, dbTipo tipo) =>
{
    TipoCliente tipoCliente = new TipoCliente();
    return tipoCliente.Alterar(id, tipo.tipo);
}).WithTags("Tipo Cliente");

app.MapPost("/tipo", (dbTipo tipo) =>
{
    TipoCliente tipoCliente = new TipoCliente();
    return tipoCliente.Inserir(tipo.tipo);
}).WithTags("Tipo Cliente");
#endregion

#region SITUAÇÃO CLIENTE
app.MapGet("/situacao/listar", () =>
{
    SituacaoCliente situacaoCliente = new SituacaoCliente();
    return situacaoCliente.Listar("", "");
}).WithTags("Situação Cliente");

app.MapDelete("/situacao/{id}", (string id) =>
{
    SituacaoCliente situacaoCliente = new SituacaoCliente();
    return situacaoCliente.Excluir(id);
}).WithTags("Situação Cliente");

app.MapPut("/situacao/{id}", (string id, dbSituacaoCliente situacao) =>
{
    SituacaoCliente situacaoCliente = new SituacaoCliente();
    return situacaoCliente.Alterar(id, situacao.situacao);
}).WithTags("Situação Cliente");

app.MapPost("/situacao", (dbSituacaoCliente situacao) =>
{
    SituacaoCliente situacaoCliente = new SituacaoCliente();
    return situacaoCliente.Inserir(situacao.situacao);
}).WithTags("Situação Cliente");
#endregion

app.Run();