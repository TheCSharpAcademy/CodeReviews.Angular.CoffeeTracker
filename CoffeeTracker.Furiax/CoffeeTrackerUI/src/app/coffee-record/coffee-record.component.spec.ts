import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeRecordComponent } from './coffee-record.component';

describe('CoffeeRecordComponent', () => {
  let component: CoffeeRecordComponent;
  let fixture: ComponentFixture<CoffeeRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CoffeeRecordComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CoffeeRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
