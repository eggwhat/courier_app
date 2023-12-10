using System.Globalization;
using System.Linq;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SwiftParcel.Services.Orders.Infrastructure.Brevo.Pdf.Models;
using SwiftParcel.Services.Orders.Core.Entities;

namespace SwiftParcel.Services.Orders.Infrastructure.Brevo.Pdf.Documents
{
    public class InvoiceDocument : IDocument
    {        
        public InvoiceModel Model { get; }

        public InvoiceDocument(InvoiceModel model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);
                    
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    
                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column
                        .Item().Text($"Invoice for order with id #{Model.OrderId}")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                    column.Item().Text(text =>
                    {
                        text.Span("Issue date: ").SemiBold();
                        text.Span($"{Model.IssueDate:d}");
                    });
                });

            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column => 
            {
                column.Spacing(20);
                
                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new AddressComponent("From", Model.Parcel.Source, Model.CustomerEmail));
                    row.ConstantItem(50);
                    row.RelativeItem().Component(new AddressComponent("For", Model.Parcel.Destination, Model.CustomerEmail));
                });

                column.Item().Element(ComposeTable);

                var totalPrice = Model.TotalPrice;
                column.Item().PaddingRight(5).AlignRight().Text($"Grand total: {totalPrice:C}").SemiBold();
            });
        }

        void ComposeTable(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold();
            
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });
                
                table.Header(header =>
                {
                    header.Cell().Text("#");
                    header.Cell().Text("Name").Style(headerStyle);
                    header.Cell().AlignRight().Text("Variant").Style(headerStyle);
                    header.Cell().AlignRight().Text("Unit price").Style(headerStyle);
                    header.Cell().AlignRight().Text("Size [cm]").Style(headerStyle);
                    header.Cell().AlignRight().Text("Weight [kg]").Style(headerStyle);
                    
                    header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });
                
                table.Cell().Element(CellStyle).Text($"{Model.Parcel.Id}");
                table.Cell().Element(CellStyle).Text($"{Model.Parcel.Name}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{Model.Parcel.Price:C}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{Model.Parcel.Width}x{Model.Parcel.Height}x{Model.Parcel.Depth}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{Model.Parcel.Weight}");            
                static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
            });
        }
    }
    
    public class AddressComponent : IComponent
    {
        private string Title { get; }
        private Address Address { get; }
        private string Email { get; }

        public AddressComponent(string title, Address address, string email)
        {
            Title = title;
            Address = address;
            Email = email;
        }
        
        public void Compose(IContainer container)
        {
            container.ShowEntire().Column(column =>
            {
                column.Spacing(2);

                column.Item().Text(Title).SemiBold();
                column.Item().PaddingBottom(5).LineHorizontal(1); 
                
                column.Item().Text(Address.Street);
                column.Item().Text($"{Address.City}, {Address.ZipCode}");
                column.Item().Text(Email);
            });
        }
    }
}
