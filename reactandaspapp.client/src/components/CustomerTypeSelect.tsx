import React from 'react';
import type { CustomerType } from '../api';

type Props = {
    types: CustomerType[];
    value?: number;
    onChange: (v: number) => void;
};

export default function CustomerTypeSelect({ types, value, onChange }: Props) {
    return (
        <select value={value ?? ''} onChange={e => onChange(Number(e.target.value))} required>
            <option value="">-- select type --</option>
            {types.map(t => <option key={t.id} value={t.id}>{t.name}</option>)}
        </select>
    );
}
