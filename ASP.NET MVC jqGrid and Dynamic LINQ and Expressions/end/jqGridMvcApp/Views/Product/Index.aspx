<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<jqGridMvcApp.Models.Product>>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <table id="ProductTable">
    </table>
    <div id="ProductTablePager">
    </div>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery('#ProductTable').jqGrid({
                url: '/Product/ProductData',
                datatype: "json",
                mtype: 'POST',
                jsonReader: {
                    page: "page",
                    total: "total",
                    records: "records",
                    root: "rows",
                    repeatitems: false,
                    id: ""
                },
                colNames: ['Id', 'Наименование', 'Категория', 'Поставщик', 'Цена', 'Количество на складе', 'Английское наименование'],
                colModel: [
                { name: 'ProductId', width: 20 },
                { name: 'ProductName', width: 150 },
                { name: 'Category', width: 100 },
                { name: 'Supplier', width: 200 },
                { name: 'UnitPrice', width: 100 },
                { name: 'UnitsInStock', width: 100 },
                { name: 'EnglishName', width: 200 }
                ],
                pager: '#ProductTablePager',
                viewrecords: true,
                height: 500
            });

            $('#ProductTable').jqGrid('filterToolbar');
        });
        
    </script>
</asp:Content>
