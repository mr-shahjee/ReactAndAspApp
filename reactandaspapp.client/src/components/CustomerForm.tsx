import React, { useState, useEffect } from 'react';
import type { Customer, CustomerType } from '../api';
import CustomerTypeSelect from './CustomerTypeSelect';

type Props = {
    types: CustomerType[];
    initial?: Customer | null;
    onSaved: () => void;
    onCancel?: () => void;
};

export default function CustomerForm({ types, initial, onSaved, onCancel }: Props) {
    const [form, setForm] = useState<Customer>(() => initial ?? {
        name: '',
        customerTypeId: 0,
        description: '',
        address: '',
        city: '',
        state: '',
        zip: ''
    } as Customer);
    useEffect(() => setForm(initial ?? ({} as Customer)), [initial]);

    const handle = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            debugger
            const payload = {
                id: form.id,
                name: form.name,
                description: form.description,
                address: form.address,
                city: form.city,
                state: form.state,
                zip: form.zip,
                lastUpdated: form.lastUpdated ?? new Date().toISOString(),
                customerTypeId: form.customerTypeId
            };
            if (form.id) {
                await fetch(`/api/customers/${form.id}`, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(payload)
                });
            } else {
                await fetch('/api/customers', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(payload)
                });
            }
            onSaved();
        } catch (err) {
            alert('Save failed: ' + (err as Error).message);
        }
    };

    return (
        <form onSubmit={handle}>
            <div>
                <label>Name</label>
                <input required value={form.name || ''} onChange={e => setForm({ ...form, name: e.target.value })} />
            </div>
            <div>
                <label>Customer Type</label>
                <CustomerTypeSelect types={types} value={form.customerTypeId} onChange={(v) => setForm({ ...form, customerTypeId: v })} />
            </div>
            <div>
                <label>Address</label>
                <input required value={form.address || ''} onChange={e => setForm({ ...form, address: e.target.value })} />
            </div>
            <div>
                <label>City</label>
                <input required value={form.city || ''} onChange={e => setForm({ ...form, city: e.target.value })} />
            </div>
            <div>
                <label>State</label>
                <input required maxLength={2} value={form.state || ''} onChange={e => setForm({ ...form, state: e.target.value })} />
            </div>
            <div>
                <label>Zip</label>
                <input required value={form.zip || ''} onChange={e => setForm({ ...form, zip: e.target.value })} />
            </div>
            <div>
                <label>Description</label>
                <textarea value={form.description || ''} onChange={e => setForm({ ...form, description: e.target.value })} />
            </div>
            <div>
                <button type="submit">Save</button>
                {onCancel && <button type="button" onClick={onCancel}>Cancel</button>}
            </div>
        </form>
    );
}
