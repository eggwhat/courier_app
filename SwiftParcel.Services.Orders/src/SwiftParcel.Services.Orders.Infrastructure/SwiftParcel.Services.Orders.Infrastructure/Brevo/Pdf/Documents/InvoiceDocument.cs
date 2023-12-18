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
                        .Item().Text($"Invoice for order with id:")
                        .FontSize(20).SemiBold().FontColor(Colors.Black);
                    column
                        .Item().Text($"#{Model.OrderId}")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Darken2);
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

                column.Item().Element(ComposeCustomerDetails);
                
                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new AddressComponent("Source", Model.Parcel.Source));
                    row.ConstantItem(50);
                    row.RelativeItem().Component(new AddressComponent("Destination", Model.Parcel.Destination));
                });

                column.Item().Element(ComposeTable);
                
                if (!string.IsNullOrWhiteSpace(Model.Parcel.Description))
                    column.Item().PaddingTop(5).Element(ComposeDescription);

                var totalPrice = Model.Parcel.CalculatedPrice;
                column.Item().PaddingRight(5).AlignRight().Text($"Grand total: {totalPrice:C}").SemiBold();
            });
        }
        
        void ComposeCustomerDetails(IContainer container)
        {
            container.ShowEntire().Column(column =>
            {
                column.Spacing(2);

                column.Item().Text("Customer details").SemiBold();
                column.Item().PaddingBottom(5).LineHorizontal(1); 

                column.Item().Text($"{Model.CustomerName}");
                column.Item().Text($"{Model.CustomerEmail}");
                column.Item().Text($"{Model.CustomerAddress.Street} {Model.CustomerAddress.BuildingNumber}" + 
                    (string.IsNullOrWhiteSpace(Model.CustomerAddress.ApartmentNumber) ? "" : $"/{Model.CustomerAddress.ApartmentNumber}"));
                column.Item().Text($"{Model.CustomerAddress.City}, {Model.CustomerAddress.ZipCode}");
                column.Item().Text($"{Model.CustomerAddress.Country}");
            });
        }

        void ComposeTable(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold();
            
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();                    
                });
                
                table.Header(header =>
                {
                    header.Cell().Text("Inquire date").Style(headerStyle);
                    header.Cell().AlignRight().Text("Size [cm]").Style(headerStyle);
                    header.Cell().AlignRight().Text("Weight [kg]").Style(headerStyle);
                    header.Cell().AlignRight().Text("Priority").Style(headerStyle);
                    header.Cell().AlignRight().Text("Delivery date").Style(headerStyle);
                    header.Cell().AlignRight().Text("Pickup date").Style(headerStyle);
                    if (Model.Parcel.AtWeekend)
                        header.Cell().AlignRight().Text("At weekend").Style(headerStyle);
                    if(Model.Parcel.VipPackage)
                        header.Cell().AlignRight().Text("Vip package").Style(headerStyle);
                    
                    header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });
                
                table.Cell().Element(CellStyle).Text($"{Model.Parcel.InquireDate}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{Model.Parcel.Width}x{Model.Parcel.Height}x{Model.Parcel.Depth}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{Model.Parcel.Weight}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{Model.Parcel.Priority}"); 
                table.Cell().Element(CellStyle).AlignRight().Text($"{Model.Parcel.DeliveryDate}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{Model.Parcel.PickupDate}"); 
                if (Model.Parcel.AtWeekend) 
                    table.Cell().Element(CellStyle).AlignRight().Text($"✓");
                if (Model.Parcel.VipPackage) 
                    table.Cell().Element(CellStyle).AlignRight().Text($"✓");         
                static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
            });
        }
        void ComposeDescription(IContainer container)
        {
            container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column => 
            {
                column.Spacing(5);
                column.Item().Text("Comments").FontSize(12).SemiBold();
                column.Item().Text(Model.Parcel.Description);
            });
        }
    }
    
    public class AddressComponent : IComponent
    {
        private string Title { get; }
        private Address Address { get; }
        public AddressComponent(string title, Address address)
        {
            Title = title;
            Address = address;
        }
        
        public void Compose(IContainer container)
        {
            container.ShowEntire().Column(column =>
            {
                column.Spacing(2);

                column.Item().Text(Title).SemiBold();
                column.Item().PaddingBottom(5).LineHorizontal(1); 

                column.Item().Text($"{Address.Street} {Address.BuildingNumber}" + 
                    (string.IsNullOrWhiteSpace(Address.ApartmentNumber) ? "" : $"/{Address.ApartmentNumber}"));
                column.Item().Text($"{Address.City}, {Address.ZipCode}");
                column.Item().Text($"{Address.Country}");
            });
        }
    }
}
