import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../Models/Product';
import { ProductRequest } from '../Models/ProductRequest';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>('http://localhost:5126/api/Product');
  }

  addProduct(newProduct: ProductRequest): Observable<Product> {
    return this.http.post<Product>(
      'http://localhost:5126/api/Product',
      newProduct
    );
  }

  updateProduct(id: string, product : ProductRequest): Observable<Product> {
    return this.http.put<Product>(
      `http://localhost:5126/api/Product/${id}`,
      product
    )
  }

  deleteProduct(id: string): Observable<JSON> {
    return this.http.delete<JSON>(
      `http://localhost:5126/api/Product/${id}`,
    )
  }
}
