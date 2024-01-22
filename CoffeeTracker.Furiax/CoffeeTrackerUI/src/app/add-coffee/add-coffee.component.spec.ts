import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCoffeeComponent } from './add-coffee.component';

describe('AddCoffeeComponent', () => {
  let component: AddCoffeeComponent;
  let fixture: ComponentFixture<AddCoffeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddCoffeeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddCoffeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
