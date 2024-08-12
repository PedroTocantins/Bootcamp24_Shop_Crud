import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../../Models/Category/Category';
import { CategoryRequest } from '../../Models/Category/CategoryRequest';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) {}

  getAll(): Observable<Category[]> {
    return this.http.get<Category[]>('http://localhost:5126/api/Category');
  }

  addCategory(newProduct: CategoryRequest): Observable<Category> {
    return this.http.post<Category>(
      'http://localhost:5126/api/Category',
      newProduct
    );
  }

  updateCategory(id: string, product : CategoryRequest): Observable<Category> {
    return this.http.put<Category>(
      `http://localhost:5126/api/Category/${id}`,
      product
    )
  }

  deleteCategory(id: string): Observable<JSON> {
    return this.http.delete<JSON>(
      `http://localhost:5126/api/Category/${id}`,
    )
  }
}
