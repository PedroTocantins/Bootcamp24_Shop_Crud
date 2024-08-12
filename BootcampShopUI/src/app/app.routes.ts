import { Routes } from '@angular/router';
import { ProductListComponent } from './features/product/product-list/product-list.component';
import { CategoryListComponent } from './features/category/category-list/category-list.component';

export const routes: Routes = [
  { path: 'admin/products', component: ProductListComponent },
  { path: 'admin/categories', component: CategoryListComponent },
];
