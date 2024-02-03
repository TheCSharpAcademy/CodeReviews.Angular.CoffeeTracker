import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchCoffeeComponent } from './search-coffee.component';

describe('SearchCoffeeComponent', () => {
  let component: SearchCoffeeComponent;
  let fixture: ComponentFixture<SearchCoffeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SearchCoffeeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SearchCoffeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
