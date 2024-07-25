import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-investimento',
  templateUrl: './investimento.component.html',
  styleUrls: ['./investimento.component.css']
})
export class InvestimentoComponent {
  valorInicial: number | undefined;
  prazo: number | undefined;
  resultado: any;

  private apiUrl = 'https://localhost:7217/api/Investimento/calcular'; // Atualize com a URL da API

  constructor(private http: HttpClient) { }

  calcularInvestimento() {
    if (this.valorInicial && this.prazo) {
      const payload = {
        valorInicial: this.valorInicial,
        prazo: this.prazo
      };

      this.http.post(this.apiUrl, payload).subscribe(response => {
        this.resultado = response;
        this.formatarResultado();
      });
    }
  }

  private formatarResultado() {
    if (this.resultado) {
      this.resultado.valorBruto = parseFloat(this.resultado.valorBruto).toFixed(2);
      this.resultado.valorLiquido = parseFloat(this.resultado.valorLiquido).toFixed(2);
    }
  }
}
