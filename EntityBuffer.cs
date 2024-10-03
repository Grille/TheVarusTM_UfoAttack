using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheVarusTM;

public class EntityBuffer<T> : IReadOnlyList<T> where T : Entity
{
    private List<T> _buffer0;
    private List<T> _buffer1;

    public EntityBuffer()
    {
        _buffer0 = new();
        _buffer1 = new();
    }

    public void Add(T entity)
    {
        _buffer0.Add(entity);
    }

    public void UpdatePositions()
    {
        for (int i = 0; i < _buffer0.Count; i++)
        {
            _buffer0[i].Position += _buffer0[i].Velocity;
        }
    }

    public void SwapBuffers()
    {
        _buffer1.Clear();
        for (int i = 0; i < _buffer0.Count; i++)
        {
            if (_buffer0[i].Alive == true)
            {
                _buffer1.Add(_buffer0[i]);
            }
        }

        var temp = _buffer0;
        _buffer0 = _buffer1;
        _buffer1 = temp;
    }

    public T this[int index] => _buffer0[index];

    public int Count => _buffer0.Count;

    public IEnumerator<T> GetEnumerator()
    {
        return _buffer0.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _buffer0.GetEnumerator();
    }
}
