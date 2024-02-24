import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCoffeeFormComponent } from './add-coffee-form.component';

describe('AddCoffeeFormComponent', () => {
  let component: AddCoffeeFormComponent;
  let fixture: ComponentFixture<AddCoffeeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddCoffeeFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddCoffeeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
