    function generate() {
      var doc = new jsPDF('p', 'pt', 'a4');

      var elem = document.getElementById('example');
      var data = doc.autoTableHtmlToJson(elem);
      doc.autoTable(data.columns, data.rows, {
                tableLineColor: [189, 195, 199],
        tableLineWidth: 0.75,
        styles: {
            font: 'Meta',
            lineColor: [44, 62, 80],
            lineWidth: 0.55
        },
        headerStyles: {
            fillColor: [0, 0, 0],
            fontSize: 11
        },
        bodyStyles: {
            fillColor: [216, 216, 216],
            textColor: 50
        },
        alternateRowStyles: {
            fillColor: [250, 250, 250]
        },
        startY: 100,
        drawRow: function (row, data) {
            // Colspan
            doc.setFontStyle('bold');
            doc.setFontSize(10);
            if ($(row.raw[0]).hasClass("innerHeader")) {
                doc.setTextColor(200, 0, 0);
                doc.setFillColor(110,214,84);
                doc.rect(data.settings.margin.left, row.y, data.table.width, 20, 'F');
                doc.autoTableText("", data.settings.margin.left + data.table.width / 2, row.y + row.height / 2, {
                    halign: 'center',
                    valign: 'middle'
                });
               /*  data.cursor.y += 20; */
            };

            if (row.index % 5 === 0) {
                var posY = row.y + row.height * 6 + data.settings.margin.bottom;
                if (posY > doc.internal.pageSize.height) {
                    data.addPage();
                }
            }
        },
        drawCell: function (cell, data) {
            // Rowspan
            console.log(cell);
            if ($(cell.raw).hasClass("innerHeader")) {
						doc.setTextColor(200, 0, 0);
                    doc.autoTableText(cell.text + '', data.settings.margin.left + data.table.width / 2, data.row.y + data.row.height / 2, {
                    halign: 'center',
                    valign: 'middle'
                });
                
                return false;
            }
        }
    }); 
     doc.save("table.pdf");
    }

$('#export').click(function (e) {
	e.preventDefault();   
    generate();
});