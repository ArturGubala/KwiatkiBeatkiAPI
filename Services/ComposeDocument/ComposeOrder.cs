using KwiatkiBeatkiAPI.Entities.Document;
using Microsoft.EntityFrameworkCore.Metadata;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace KwiatkiBeatkiAPI.Services.ComposeDocument
{
    public class ComposeOrder : IDocument
    {
        private readonly DocumentEntity _documentEntity;

        public ComposeOrder(DocumentEntity documentEntity)
        {
            _documentEntity = documentEntity;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(30);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);


                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Strona ").FontSize(7);
                        x.CurrentPageNumber().FontSize(7);
                        x.Span(" z ").FontSize(7);
                        x.TotalPages().FontSize(7);
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor("#c89a7b");

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"{_documentEntity.DocumentType.Name} nr").Style(titleStyle);
                    column.Item().Text($"{_documentEntity.FullDocumentNumber}").Style(titleStyle);
                    column.Item().PaddingVertical(5).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten1);

                    column.Item().Text(text =>
                    {
                        text.Span("Data utworzenia: ");
                        text.Span($"{_documentEntity.Created:d}").SemiBold();
                    });

                    if (_documentEntity.WarehouseFromId != null && _documentEntity.WarehouseToId != null)
                    {
                        column.Item().Text(text =>
                        {
                            text.Span("Z magazynu: ");
                            text.Span($"{_documentEntity.WarehouseFrom!.Name} ({_documentEntity.WarehouseFrom!.Code})").SemiBold();
                            text.EmptyLine();
                            text.Span("Na magazynu: ");
                            text.Span($"{_documentEntity.WarehouseTo!.Name} ({_documentEntity.WarehouseTo!.Code})").SemiBold();
                        });
                    }
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);
                column.Item().Element(ComposeTable);

                if (!string.IsNullOrWhiteSpace(_documentEntity.Remarks))
                    column.Item().PaddingTop(15).Element(ComposeComments);

                column.Item().Element(ComposeSignatureElement);
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                TextStyle titleStyle = TextStyle.Default.FontSize(7);

                IContainer DefaultCellStyle(IContainer container, string backgroundColor)
                {
                    return container
                         .Border(0.5f)
                         .BorderColor(Colors.Grey.Lighten1)
                         .Background(backgroundColor)
                         .PaddingVertical(3)
                         .PaddingHorizontal(5)
                         .DefaultTextStyle(titleStyle);
                }

                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(9);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                table.Header(header =>
                {
                    header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignMiddle().Text("Lp.");
                    header.Cell().RowSpan(2).Element(CellStyle).PaddingVertical(0).AlignCenter().AlignMiddle().Text(text =>
                    {
                        text.Line("Asortyment");
                        text.Line("Nazwa wewnętrzna | Uwagi do pozycji").FontSize(6).Italic().LineHeight(0.5f);
                    });
                    header.Cell().ColumnSpan(2).Element(CellStyle).AlignCenter().Text("Ilość");
                    header.Cell().Element(CellStyle).AlignCenter().Text("Sztuki");
                    header.Cell().Element(CellStyle).AlignCenter().Text("Zgrzewki / kartony");

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten4);
                });

                foreach (var line in _documentEntity.Lines)
                {
                    table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text($"{_documentEntity.Lines.ToList().IndexOf(line) + 1}");
                    table.Cell().Element(CellStyle).AlignLeft().Text(text =>
                    {
                        text.Span(line.Item.StockCode);
                        if (!(string.IsNullOrWhiteSpace(line.Item.Alias) && string.IsNullOrWhiteSpace(line.Remarks)))
                        {
                            text.Span($"\n{line.Item.Alias ?? "brak"} | {line.Remarks ?? "brak"}").FontSize(6).Italic();
                        }
                    });
                    table.Cell().Element(CellStyle).AlignRight().AlignMiddle().Text($"{line.Quantity}");
                    table.Cell().Element(CellStyle).AlignRight().AlignMiddle().Text($"{line.Quantity / decimal.Parse(line.Item.ItemProperties.First(ip => ip.PropertyId == 7).Value)}");

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                }
            });
        }

        void ComposeComments(IContainer container)
        {
            container.Background(Colors.Grey.Lighten3).Padding(5).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Uwagi do dokumentu").FontSize(7);
                column.Item().Text(_documentEntity.Remarks).FontSize(7);
            });
        }

        void ComposeSignatureElement(IContainer container)
        {
            container.PaddingTop(50).Row(row =>
            {
                row.Spacing(50);
                row.RelativeItem(5).PaddingHorizontal(50).Column(column =>
                {
                    column.Item().PaddingBottom(10).AlignCenter().Text($"Podpis osoby upowarznionej").FontSize(7).FontColor(Colors.Grey.Lighten1);
                    column.Item().LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten1);
                    column.Item().PaddingTop(5).AlignCenter().Text("Odebrał/a").FontSize(7);
                });
                row.RelativeItem(5).PaddingHorizontal(50).Column(column =>
                {
                    column.Item().PaddingBottom(10).AlignCenter().Text($"{_documentEntity.User.FirstName} {_documentEntity.User.LastName}").FontSize(7).FontColor(Colors.Grey.Lighten1);
                    column.Item().LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten1);
                    column.Item().PaddingTop(5).AlignCenter().Text("Wystawił/a").FontSize(7);
                });
            });
        }
    }
}
